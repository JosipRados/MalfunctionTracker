using Microsoft.Extensions.Caching.Memory;

namespace MalfunctionTrackerData.DataAccess
{
    public class MongoPriorityData : IPriorityData
    {
        private readonly IMemoryCache _cache;
        private readonly IMongoCollection<PriorityModel> _priorities;
        private const string CacheName = "priorityData";



        public MongoPriorityData(IDBConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _priorities = db.PriorityCollection;
        }

        public async Task<List<PriorityModel>> GetAllPriorities()
        {
            var output = _cache.Get<List<PriorityModel>>(CacheName);
            if (output is null)
            {
                var results = await _priorities.FindAsync(_ => true);
                output = results.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromDays(1));
            }
            return output;
        }

        public Task CreatePriority(PriorityModel priority)
        {
            return _priorities.InsertOneAsync(priority);
        }
    }
}
