using Microsoft.Extensions.Caching.Distributed;
using RedisCache.API.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisCache.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly ILogger<CacheService> _logger;
        public CacheService(IConnectionMultiplexer redis, ILogger<CacheService> logger)
        {
            _db = redis.GetDatabase();
            _logger = logger;
        }
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await _db.StringSetAsync(key, json, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SetAsync error: {ex.Message}.");
            }
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                var value = await _db.StringGetAsync(key);
                if (value.IsNullOrEmpty) return default;

                return JsonSerializer.Deserialize<T>(value!);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAsync error: {ex.Message}.");
                return default;
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }
    }
}
