
using Application.Common.Interfaces;
using Asp.Versioning;
using Infrastructure.Caching;
using Infrastructure.OpenApi;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddApiVersioning(configuration)
                .AddPersistence(configuration)
                .AddUnitOfWork()
                .AddHealthCheck()
                .AddOpenApiDocumentation(configuration)
                .AddCaching()
                .AddRedis(configuration);
        }

        private static IServiceCollection AddApiVersioning(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(int.Parse(cfg["Versions:MajorApiVersion"] ?? "1"), int.Parse(cfg["Versions:MinorApiVersion"] ?? "0"));
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, UnitOfWork<ECommerceDbContext>>();

        private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
       services.AddHealthChecks().Services;

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
            builder
            .UseHttpsRedirection()
            .UseAuthorization()
            .UseRouting()
            .UseOpenApiDocumentation(config);

        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapControllers();
            builder.MapHealthCheck();
            return builder;
        }
        private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health");
    }
}