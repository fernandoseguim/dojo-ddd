using Amaury.Abstractions;
using DojoDDD.Api.Extensions.Configurations;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Infra.DbContext;
using DojoDDD.Infra.DbContext.Models;
using DojoDDD.Infra.DbContext.RavenDb;
using DojoDDD.Infra.DbContext.RavenDb.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;

namespace DojoDDD.Api.Extensions.DbContext.RavenDb
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRavenDbRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbContextOptions>(configuration.GetSection("DbContext"));

            var connection = configuration.GetRavenDbClusterConnection();

            services.AddSingleton<IDocumentStore>(new DocumentStore
            {
                    Urls = connection.Endpoints
            }.Initialize());

            services.AddScoped<IDatabaseContext<IDocumentStore>, RavenDbDatabaseContext>();
            services.AddScoped<IDatabaseContext<IDocumentStore>, RavenDbCelebrityEventStoreContext>();
            services.AddAsyncInitializer<DatabaseContextInitializer>();

            services.AddCelebrityEventStore<PurchaseOrder, RavenDbEventStoreModel>();

            services.AddScoped<IQueryableRepository<ClientModel>, ClientRavenDbRepository>();
            services.AddScoped<IQueryableRepository<ProductModel>, ProductsRavenDbRepository>();
            services.AddScoped<IQueryableRepository<PurchaseOrderModel>, PurchaseOrderEventStoreRavenDbRepository>();

            services.AddScoped<IEntityRepository<Client>, ClientRavenDbRepository>();
            services.AddScoped<IEntityRepository<Product>, ProductsRavenDbRepository>();
            services.AddScoped<IEntityRepository<PurchaseOrder>, PurchaseOrderEventStoreRavenDbRepository>();

            return services;
        }

        private static IServiceCollection AddCelebrityEventStore<TCelebrity, TEventStoreModel>(this IServiceCollection services)
                where TCelebrity : CelebrityAggregateBase
                where TEventStoreModel : IRavenDbEventStoreModel, new()
        {
            services.AddScoped<ICelebrityEventStore<TCelebrity, TEventStoreModel>, RavenDbCelebrityEventStore<TCelebrity, TEventStoreModel>>();

            return services;
        }
    }
}