using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Infra.DbContext.InMemory;
using DojoDDD.Infra.DbContext.InMemory.Repositories;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DojoDDD.Api.Extensions.DbContext.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
        {
            services.AddSingleton<DataStore>();

            services.AddSingleton<IQueryableRepository<ClientModel>, ClientInMemoryRepository>();
            services.AddSingleton<IQueryableRepository<ProductModel>, ProductsInMemoryRepository>();
            services.AddSingleton<IQueryableRepository<PurchaseOrderModel>, PurchaseOrderInMemoryRepository>();

            services.AddSingleton<IEntityRepository<Client>, ClientInMemoryRepository>();
            services.AddSingleton<IEntityRepository<Product>, ProductsInMemoryRepository>();
            services.AddSingleton<IEntityRepository<PurchaseOrder>, PurchaseOrderInMemoryRepository>();

            return services;
        }
    }
}