using System.Diagnostics.CodeAnalysis;
using DojoDDD.Api.Extensions.Configurations;
using DojoDDD.Domain.Abstractions.Factories;
using DojoDDD.Infra.Factories;
using Hangfire;
using Hangfire.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DojoDDD.Api.Extensions.Cache
{
    [ExcludeFromCodeCoverage]
    public static class DistributedCacheExtensions
    {
        public static void AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redis = configuration.GetRedisClusterConnection();
            var connection = ConnectionMultiplexer.Connect(redis.Host);
            var options = new RedisStorageOptions { Prefix = redis.Prefixes["Hangfire"] };
            services.AddHangfire(c => c.UseRedisStorage(connection, options));

            var database = connection.GetDatabase(1);
            services.AddSingleton(_ => database);

            services.AddSingleton<ISequenceNumberFactory, RedisSequenceNumberFactory>();
        }
    }
}