

using Infrastructure.OpenApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.OpenApi
{

    [ExcludeFromCodeCoverage]
    internal static class Startup
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            if (settings == null)
            {
                return services;
            }

            if (!settings.Enable)
            {
                return services;
            }

            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument((document, _) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = settings.Title;
                    doc.Info.Version = settings.Version;
                    doc.Info.Description = settings.Description;
                    doc.Info.Contact = new()
                    {
                        Name = settings.ContactName,
                        Email = settings.ContactEmail,
                        Url = settings.ContactUrl
                    };
                    doc.Info.License = new()
                    {
                        Name = settings.LicenseName,
                        Url = settings.LicenseUrl
                    };
                };
            });

            return services;
        }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            if (!config.GetValue<bool>("SwaggerSettings:Enable"))
            {
                return app;
            }

            app.UseOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DefaultModelsExpandDepth = -1;
                options.DocExpansion = "none";
                options.TagsSorter = "alpha";
            });

            return app;
        }
    }
}
