using DojoDDD.Api.Extensions.DbContext.RavenDb;
using DojoDDD.Api.Extensions.MassTransit;
using DojoDDD.Infra.DbContext;
using Microsoft.Extensions.Configuration;

namespace DojoDDD.Api.Extensions.Configurations
{
    public static class ConfigurationExtensions
    {
        public static DbContextOptions GetDbContext(this IConfiguration configuration)
            => configuration.GetSection("DbContext").Get<DbContextOptions>();

        public static RabbitMqClusterConnection GetRabbitMqClusterConnection(this IConfiguration configuration)
            => configuration.GetSection("Connections:RabbitMq").Get<RabbitMqClusterConnection>();

        public static RavenDbClusterConnection GetRavenDbClusterConnection(this IConfiguration configuration)
            => configuration.GetSection("Connections:RavenDb").Get<RavenDbClusterConnection>();

        public static RedisClusterConnection GetRedisClusterConnection(this IConfiguration configuration)
            => configuration.GetSection("Connections:Redis").Get<RedisClusterConnection>();
    }
}
