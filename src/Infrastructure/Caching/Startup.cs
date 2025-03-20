using Application.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Caching
{
    public static class Startup
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            return services
                .AddMemoryCache()
                .AddSingleton<ICacheService, CacheService>();
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("Redis");
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            return services;
        }
    }
}
