using System;
using System.Threading.Tasks;
using ValhallaManagement.Core.Interfaces;

namespace ValhallaManagement.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheService _cacheImplementation;

        public CacheService(ICacheService cacheImplementation)
        {
            _cacheImplementation = cacheImplementation;
        }

        public async Task<T> GetCachedDataAsync<T>(string key)
        {
            return await _cacheImplementation.GetCachedDataAsync<T>(key);
        }

        public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan timeToLive)
        {
            await _cacheImplementation.SetCachedDataAsync(key, data, timeToLive);
        }

        public async Task RemoveCachedDataAsync(string key)
        {
            await _cacheImplementation.RemoveCachedDataAsync(key);
        }
    }
}
