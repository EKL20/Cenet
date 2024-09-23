using ValhallaManagement.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace ValhallaManagement.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task<T> GetCachedDataAsync<T>(string key)
        {
            var db = _redisConnection.GetDatabase();
            var cachedData = await db.StringGetAsync(key);

            if (cachedData.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan timeToLive)
        {
            var db = _redisConnection.GetDatabase();
            var serializedData = JsonSerializer.Serialize(data);
            await db.StringSetAsync(key, serializedData, timeToLive);
        }

        public async Task RemoveCachedDataAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
