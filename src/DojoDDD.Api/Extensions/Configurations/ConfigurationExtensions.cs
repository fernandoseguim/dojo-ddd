using DojoDDD.Api.Extensions.MassTransit;
using Microsoft.Extensions.Configuration;

namespace DojoDDD.Api.Extensions.Configurations
{
    public static class ConfigurationExtensions
    {
        public static RabbitMqClusterConfiguration GetRabbitMqClusterConfiguration(this IConfiguration configuration)
            => configuration.GetSection("Rabbit").Get<RabbitMqClusterConfiguration>();
    }
}
