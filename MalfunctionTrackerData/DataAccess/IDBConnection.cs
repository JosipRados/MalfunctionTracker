using MongoDB.Driver;

namespace MalfunctionTrackerData.DataAccess
{
    public interface IDBConnection
    {
        IMongoCollection<CategoryModel> CategoryCollection { get; }
        string CategoryCollectionName { get; }
        MongoClient Client { get; }
        string DbName { get; }
        IMongoCollection<MalfunctionModel> MalfunctionCollection { get; }
        string MalfunctionCollectionName { get; }
        IMongoCollection<StatusModel> StatusCollection { get; }
        string StatusCollectionName { get; }
        IMongoCollection<UserModel> UserCollection { get; set; }
        string UserCollectionName { get; }
        string PriorityCollectionName { get; }
        IMongoCollection<PriorityModel> PriorityCollection { get; }
    }
}