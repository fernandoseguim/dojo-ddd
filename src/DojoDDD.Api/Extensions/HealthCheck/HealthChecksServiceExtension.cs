using System.Diagnostics.CodeAnalysis;
using DojoDDD.Api.Extensions.Configurations;
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
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddRavenDB(options => options.Urls = configuration.GetRavenDbClusterConnection().Endpoints, "ravendb", HealthStatus.Unhealthy, new []{"ready", "ravendb"})
                    .AddRedis(configuration.GetRedisClusterConnection().Host, "redis", HealthStatus.Unhealthy, new []{"ready", "redis"})
                    .AddHangfire(options =>
                    {
                        options.MinimumAvailableServers = 1;
                        options.MaximumJobsFailed = 3;
                    }, "hangfire", HealthStatus.Degraded, new []{"ready", "hangfire"});

            services.AddHealthChecksUI(options =>
                    {
                        options.SetEvaluationTimeInSeconds(5);
                        options.AddHealthCheckEndpoint("DojoDDD.Api", "http://localhost:23001/hc");
                    })
                    .AddInMemoryStorage();

            return services;
        }
    }
}