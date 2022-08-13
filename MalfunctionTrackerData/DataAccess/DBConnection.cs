using MalfunctionTrackerData.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace MalfunctionTrackerData.DataAccess
{
    public class DBConnection : IDBConnection
    {
        #region "Properties"
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _db;
        private string _connectionId = "MongoDB";

        public string DbName { get; private set; }
        public string CategoryCollectionName { get; private set; } = "categories";
        public string StatusCollectionName { get; private set; } = "statuses";
        public string UserCollectionName { get; private set; } = "users";
        public string MalfunctionCollectionName { get; private set; } = "malfunctions";
        public string PriorityCollectionName { get; private set; } = "priorities";
        public MongoClient Client { get; private set; }
        public IMongoCollection<CategoryModel> CategoryCollection { get; private set; }
        public IMongoCollection<StatusModel> StatusCollection { get; private set; }
        public IMongoCollection<UserModel> UserCollection { get; set; }
        public IMongoCollection<MalfunctionModel> MalfunctionCollection { get; private set; }
        public IMongoCollection<PriorityModel> PriorityCollection { get; private set; }
        #endregion

        public DBConnection(IConfiguration config)
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString(_connectionId));
            DbName = _config["DatabaseName"];
            _db = Client.GetDatabase(DbName);

            CategoryCollection = _db.GetCollection<CategoryModel>(CategoryCollectionName);
            StatusCollection = _db.GetCollection<StatusModel>(StatusCollectionName);
            UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
            MalfunctionCollection = _db.GetCollection<MalfunctionModel>(MalfunctionCollectionName);
            PriorityCollection = _db.GetCollection<PriorityModel>(PriorityCollectionName);
        }
    }
}
