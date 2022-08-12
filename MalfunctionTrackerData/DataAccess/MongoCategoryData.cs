using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalfunctionTrackerData.DataAccess
{
    public class MongoCategoryData : ICategoryData
    {
        private readonly IMemoryCache _cache;
        private readonly IMongoCollection<CategoryModel> _categories;
        private const string CacheName = "categoryData";



        public MongoCategoryData(IDBConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _categories = db.CategoryCollection;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var output = _cache.Get<List<CategoryModel>>(CacheName);
            if (output is null)
            {
                var results = await _categories.FindAsync(_ => true);
                output = results.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromDays(1));
            }
            return output;
        }

        public Task CreateCategory(CategoryModel category)
        {
            return _categories.InsertOneAsync(category);
        }
    }
}
