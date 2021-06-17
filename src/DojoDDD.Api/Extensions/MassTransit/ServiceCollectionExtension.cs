using System;
using System.Diagnostics.CodeAnalysis;
using DojoDDD.Api.Consumers;
using DojoDDD.Api.Extensions.Configurations;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Events;
using DojoDDD.Infra.Providers.Schedulers;
using Hangfire;
using Hangfire.Redis;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DojoDDD.Api.Extensions.MassTransit
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {
        public static void AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var redis = configuration.GetRedisClusterConnection();
            var connection = ConnectionMultiplexer.Connect(redis.Host);
            var options = new RedisStorageOptions { Prefix = redis.Prefixes["Hangfire"] };
            services.AddHangfire(c => c.UseRedisStorage(connection, options));

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<PurchaseOrderWasRequestedConsumer>();
                configure.AddConsumer<PurchaseOrderProcessingCommandConsumer>();

                configure.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(bus =>
                {
                    var rabbitConfiguration = configuration.GetRabbitMqClusterConnection();
                    var uri = new Uri(rabbitConfiguration.Host);

                    bus.Host(uri, rabbitMq =>
                    {
                        rabbitMq.Username(rabbitConfiguration.Username);
                        rabbitMq.Password(rabbitConfiguration.Password);
                        rabbitMq.UseCluster(cluster => cluster.AddNodes(rabbitConfiguration.Nodes));
                    });

                    bus.Message<IEvent<PurchaseOrder>>(configurator => configurator.SetEntityName("purchase.order.event"));
                    bus.Message<PurchaseOrderWasRequested>(configurator => configurator.SetEntityName("purchase.order.was.requested"));
                    bus.Message<PurchaseOrderProcessingCommand>(configurator => configurator.SetEntityName("purchase.order.processing.command"));

                    bus.UseHangfireScheduler(new ServiceProviderHangfireComponentResolver(provider));

                    bus.ReceiveEndpoint("purchase_order_events", endpoint =>
                    {
                        endpoint.Consumer<PurchaseOrderWasRequestedConsumer>(provider);
                    });

                    bus.ReceiveEndpoint("purchase_order_commands", endpoint =>
                    {
                        endpoint.Consumer<PurchaseOrderProcessingCommandConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();

            services.AddScoped<ICommandScheduleProvider<PurchaseOrderProcessingCommand>, MassTransitSchedulerProvider>();
        }

        private static void AddNodes(this IRabbitMqClusterConfigurator cluster, params string[] nodes)
        {
            foreach(var node in nodes) cluster.Node(node);
        }
    }
}