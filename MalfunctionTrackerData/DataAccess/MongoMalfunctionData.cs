using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalfunctionTrackerData.DataAccess
{
    public class MongoMalfunctionData : IMalfunctionData
    {
        private readonly IDBConnection _db;
        private readonly IUserData _userData;
        private readonly IMemoryCache _cache;
        private readonly IMongoCollection<MalfunctionModel> _malfunction;
        private const string CacheName = "MalfunctionData";

        public MongoMalfunctionData(IDBConnection db, IUserData userData, IMemoryCache cache)
        {
            _db = db;
            _userData = userData;
            _cache = cache;
            _malfunction = db.MalfunctionCollection;
        }

        public async Task<List<MalfunctionModel>> GetAllMalfunctions()
        {
            var output = _cache.Get<List<MalfunctionModel>>(CacheName);
            if (output is null)
            {
                var result = await _malfunction.FindAsync(m => m.Archived == false);
                output = result.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
            }
            return output;
        }

        public async Task<List<MalfunctionModel>> GetUsersMalfunctions(string userId)
        {
            var output = _cache.Get<List<MalfunctionModel>>(userId);
            if(output is null)
            {
                var results = await _malfunction.FindAsync(m => m.User.Id == userId);
                output = results.ToList();

                _cache.Set(userId, output, TimeSpan.FromMinutes(1));
            }

            return output;
        }

        public async Task<List<MalfunctionModel>> GetAllAcknowledgedMalfunctions()
        {
            var output = await GetAllMalfunctions();
            return output.Where(m => m.Acknowledged == true).ToList();
        }

        public async Task<MalfunctionModel> GetMalfunction(string id)
        {
            var result = await _malfunction.FindAsync(m => m.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<List<MalfunctionModel>> GetAllMalfunctionsWaitingAcknowledgement()
        {
            var output = await GetAllMalfunctions();
            return output.Where(m => m.Acknowledged == false && m.Rejected == false).ToList();
        }

        public async Task UpdateMalfunction(MalfunctionModel malfunction)
        {
            await _malfunction.ReplaceOneAsync(m => m.Id == malfunction.Id, malfunction);
            _cache.Remove(CacheName); //Prisiljavam da sljedeći korisnik koji pita kvarove uzima iz baze i kešira update verziju
        }

        public async Task CreateMalfunction(MalfunctionModel malfunction)
        {
            var client = _db.Client;
            using var session = await client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                var db = client.GetDatabase(_db.DbName);
                var malfunctionInTransaction = db.GetCollection<MalfunctionModel>(_db.MalfunctionCollectionName);
                await malfunctionInTransaction.InsertOneAsync(malfunction);

                var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
                var user = await _userData.GetUser(malfunction.User.Id);
                user.AuthoredMalfunctions.Add(new BasicMalfunctionModel(malfunction));
                await userInTransaction.ReplaceOneAsync(u => u.Id == user.Id, user);

                await session.CommitTransactionAsync();

                //Ne čistim cache jer svakako kvar mora proći aprove fazu
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
