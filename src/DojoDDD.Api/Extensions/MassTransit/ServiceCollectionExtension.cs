using System;
using System.Diagnostics.CodeAnalysis;
using DojoDDD.Api.Consumers;
using DojoDDD.Api.Extensions.Configurations;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.PurchaseOrders.Events.StateTransfer;
using DojoDDD.Infra.Providers.Schedulers;
using MassTransit;
using MassTransit.KafkaIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DojoDDD.Api.Extensions.MassTransit
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {
        public static void AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<PurchaseOrderProcessingCommandConsumer>();

                configure.AddRider(rider =>
                {
                    rider.AddConsumer<PurchaseOrderWasRequestedConsumer>();
                    rider.AddProducer<IEvent<PurchaseOrder>>("purchase_order_events");

                    rider.UsingKafka((context, config) =>
                    {
                        config.Host("localhost:9092", host => { });

                        config.TopicEndpoint<IEvent<PurchaseOrder>>("purchase_order_events", "dojoddd.api", topic =>
                        {
                            topic.CreateIfMissing(options =>
                            {
                                options.NumPartitions = 2;
                                options.ReplicationFactor = 1;
                            });

                            topic.ConfigureConsumer<PurchaseOrderWasRequestedConsumer>(context);
                        });
                    });
                });

                configure.UsingRabbitMq((context, bus) =>
                {
                    var rabbitConfiguration = configuration.GetRabbitMqClusterConnection();
                    var uri = new Uri(rabbitConfiguration.Host);

                    bus.Host(uri, host =>
                    {
                        host.Username(rabbitConfiguration.Username);
                        host.Password(rabbitConfiguration.Password);
                        host.UseCluster(cluster => cluster.AddNodes(rabbitConfiguration.Nodes));
                    });

                    bus.Message<IEvent<PurchaseOrder>>(configurator => configurator.SetEntityName("purchase.order.event"));
                    bus.Message<PurchaseOrderWasRequested>(configurator => configurator.SetEntityName("purchase.order.was.requested"));
                    bus.Message<PurchaseOrderProcessingCommand>(configurator => configurator.SetEntityName("purchase.order.processing.command"));

                    bus.UseHangfireScheduler(new ServiceProviderHangfireComponentResolver(context));

                    // bus.ReceiveEndpoint("purchase_order_events", endpoint =>
                    // {
                    //     endpoint.Consumer<PurchaseOrderWasRequestedConsumer>(context);
                    // });

                    bus.ReceiveEndpoint("purchase_order_commands", endpoint =>
                    {
                        endpoint.Consumer<PurchaseOrderProcessingCommandConsumer>(context);
                    });
                });
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