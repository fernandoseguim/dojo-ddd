using System.Diagnostics.CodeAnalysis;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

namespace DojoDDD.Api.Extensions.HealthCheck
{
    [ExcludeFromCodeCoverage]
    public static class HealthChecksMiddlewareExtension
    {
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks(new PathString("/hc"), new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });

            return builder;
        }
    }
}