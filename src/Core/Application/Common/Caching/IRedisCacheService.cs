using StackExchange.Redis;

namespace Application.Common.Caching
{
    public interface IRedisCacheService
    {
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan? slidingExpiration = null);
        Task<T> GetCacheValueAsync<T>(string key);
        Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[] keys, RedisValue[] values);
        Task<IDictionary<string, T?>> GetCacheValuesBatchAsync<T>(string[] keys);

        IBatch CreateBatch();

        Task ExecuteBatchAsync(IBatch batch);

        Task<bool> LockAsync(string key, string value, TimeSpan expiration);

        Task UnlockAsync(string key, string value);
    }
}
