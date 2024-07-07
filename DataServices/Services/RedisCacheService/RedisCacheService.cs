using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.RedisCacheService
{
    public class RedisCacheService : IRedisCacheService
    {

        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
        }

        public async Task<bool> SetAsync(string key, string value)
        {
            var db = _redis.GetDatabase();
            return await db.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var db = _redis.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}
