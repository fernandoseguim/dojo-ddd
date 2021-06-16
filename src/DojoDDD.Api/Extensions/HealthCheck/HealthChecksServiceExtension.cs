using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DojoDDD.Api.Extensions.HealthCheck
{
    [ExcludeFromCodeCoverage]
    public static class HealthChecksServiceExtension
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}