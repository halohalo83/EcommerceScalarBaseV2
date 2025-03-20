using System.Diagnostics.CodeAnalysis;

namespace EcommerceScalarBase.Configurations
{
    [ExcludeFromCodeCoverage]
    internal static class Startup
    {
        internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            const string configurationsDirectory = "Configurations";
            var env = builder.Environment;
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/openapi.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/openapi.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            return builder;
        }
    }
}
