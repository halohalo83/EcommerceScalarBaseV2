using Application.Common.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Caching
{
    public class CacheService(IMemoryCache cache, ILogger<CacheService> logger) : ICacheService
    {
        public T? Get<T>(string key) =>
       cache.Get<T>(key);

        public Task<T?> GetAsync<T>(string key, CancellationToken token = default) =>
            Task.FromResult(Get<T>(key));

        public void Refresh(string key) =>
            cache.TryGetValue(key, out _);

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            Refresh(key);
            return Task.CompletedTask;
        }

        public void Remove(string key) =>
            cache.Remove(key);

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            Remove(key);
            return Task.CompletedTask;
        }

        public void Set<T>(string key, T value, TimeSpan? slidingExpiration = null)
        {
            slidingExpiration ??= TimeSpan.FromMinutes(10); // Default expiration time of 10 minutes.

            cache.Set(key, value, new MemoryCacheEntryOptions { SlidingExpiration = slidingExpiration });
            logger.LogDebug("Added to Cache : {0}", key);
        }

        public Task SetAsync<T>(string key, T value)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.MaxValue
            };

            cache.Set(key, value, options);

            logger.LogDebug("Added to Cache : {0}", key);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Set(key, value, slidingExpiration);
            return Task.CompletedTask;
        }
    }
}
