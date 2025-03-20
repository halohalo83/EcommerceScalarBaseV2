using Application.Common.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Caching
{
    public class RedisCacheService(IConnectionMultiplexer redis) : IRedisCacheService
    {
        public IBatch CreateBatch() {
            var db = redis.GetDatabase();
            return db.CreateBatch();
        }

        public async Task ExecuteBatchAsync(IBatch batch)
        {
            batch.Execute();
            await Task.CompletedTask;
        }
        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? slidingExpiration = null)
        {
            slidingExpiration ??= TimeSpan.FromMinutes(10); // Default expiration time of 10 minutes.

            var db = redis.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, slidingExpiration);
        }

        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            var db = redis.GetDatabase();
            var json = await db.StringGetAsync(key);
            return json.HasValue ? JsonSerializer.Deserialize<T>(json) : default;
        }

        public async Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[] keys, RedisValue[] values)
        {
            var db = redis.GetDatabase();
            return await db.ScriptEvaluateAsync(script, keys, values);
        }

        public async Task<IDictionary<string, T?>> GetCacheValuesBatchAsync<T>(string[] keys)
        {
            var db = redis.GetDatabase();

            if (keys == null || keys.Length == 0)
                return new Dictionary<string, T?>();

            // Convert string keys to RedisKey array
            RedisKey[] redisKeys = keys.Select(k => (RedisKey)k).ToArray();

            // Fetch all values in a single batch call
            RedisValue[] values = await db.StringGetAsync(redisKeys);

            // Map results to a dictionary
            var result = new Dictionary<string, T?>();

            for (int i = 0; i < keys.Length; i++)
            {
                if (values[i].HasValue)
                {
                    result[keys[i]] = JsonSerializer.Deserialize<T>(values[i]);
                }
                else
                {
                    result[keys[i]] = default;
                }
            }

            return result;
        }

        public async Task<bool> LockAsync(string key, string value, TimeSpan expiration)
        {
            var db = redis.GetDatabase();
            return await db.StringSetAsync(key, value, expiration, When.NotExists);
        }

        public async Task UnlockAsync(string key, string value)
        {
            var db = redis.GetDatabase();
            var script = @"
                if redis.call('get', KEYS[1]) == ARGV[1] then
                    return redis.call('del', KEYS[1])
                else
                    return 0
                end";
            await db.ScriptEvaluateAsync(script, new RedisKey[] { key }, new RedisValue[] { value });
        }
    }
}
