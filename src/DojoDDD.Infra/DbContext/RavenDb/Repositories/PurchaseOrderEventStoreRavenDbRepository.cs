using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Infra.DbContext.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DojoDDD.Infra.DbContext.RavenDb.Repositories
{
    public class PurchaseOrderEventStoreRavenDbRepository : IEntityRepository<PurchaseOrder>, IQueryableRepository<PurchaseOrderModel>
    {
        private readonly ICelebrityEventStore<PurchaseOrder, RavenDbEventStoreModel> _store;

        public PurchaseOrderEventStoreRavenDbRepository(ICelebrityEventStore<PurchaseOrder, RavenDbEventStoreModel> store) => _store = store;

        public async Task<PurchaseOrder> GetAsync(string id)
        {
            var product = await _store.LoadAsync(IdHelper.LoadForOrders(id), CancellationToken.None);

            return product;
        }

        public async Task<PurchaseOrderModel> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            var orders = await GetManyAsync(spec);
            return orders.FirstOrDefault();
        }

        public async Task<ICollection<PurchaseOrderModel>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            // var products = await _session.Query<PurchaseOrderModel>()
            //         .Where(spec.Expression)
            //         .Include<PurchaseOrderModel, Client>(order => order.Client.Id)
            //         .Include<PurchaseOrderModel, Product>(order => order.Product.Id)
            //         .ToListAsync();

            return default;
        }

        public async Task<ICollection<PurchaseOrderModel>> GetAllAsync()
        {
            // var products = await _session.Query<PurchaseOrderModel>()
            //         .Include<PurchaseOrderModel, Client>(order => order.Client.Id)
            //         .Include<PurchaseOrderModel, Product>(order => order.Product.Id)
            //         .ToListAsync();

            return default;
        }

        public async Task SaveAsync(PurchaseOrder entity) => await _store.CommitAsync(entity, CancellationToken.None);
    }
}