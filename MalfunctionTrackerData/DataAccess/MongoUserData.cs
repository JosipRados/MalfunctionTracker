namespace MalfunctionTrackerData.DataAccess
{
    public class MongoUserData : IUserData
    {
        private readonly IMongoCollection<UserModel> _users;
        public MongoUserData(IDBConnection db)
        {
            _users = db.UserCollection;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            //_ znaći da gleda svaki record a pošto je => true uzima svaki record iz baze
            var results = await _users.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<UserModel> GetUser(string id)
        {
            var user = await _users.FindAsync(u => u.Id == id);
            return user.FirstOrDefault();
        }

        public async Task<UserModel> GetUserFromAuthentication(string objectId)
        {
            var user = await _users.FindAsync(u => u.ObjectIdentifier == objectId);
            return user.FirstOrDefault();
        }

        public Task CreateUser(UserModel user)
        {
            return _users.InsertOneAsync(user);
        }

        public Task UpdateUser(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            //traži usera po filteru i ako postoji zamjenin ako ne postoji napravi insert usera
            return _users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }
    }
}
