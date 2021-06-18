using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Infra.DbContext.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DojoDDD.Infra.DbContext.RavenDb.Repositories
{
    public class PurchaseOrderRavenDbRepository : IEntityRepository<PurchaseOrder>, IQueryableRepository<PurchaseOrderModel>, IDisposable
    {
        private readonly IAsyncDocumentSession _session;

        public PurchaseOrderRavenDbRepository(IDatabaseContext<IDocumentStore> context) => _session = context.Store.OpenAsyncSession();

        public async Task<PurchaseOrder> GetAsync(string id)
        {
            var product = await _session.LoadAsync<PurchaseOrderModel>(IdHelper.LoadForOrders(id));

            return (PurchaseOrder) product;
        }

        public async Task<PurchaseOrderModel> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            var orders = await GetManyAsync(spec);
            return orders.FirstOrDefault();
        }

        public async Task<ICollection<PurchaseOrderModel>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var products = await _session.Query<PurchaseOrderModel>()
                    .Where(spec.Expression)
                    .Include<PurchaseOrderModel, Client>(order => order.Client.Id)
                    .Include<PurchaseOrderModel, Product>(order => order.Product.Id)
                    .ToListAsync();

            return products;
        }

        public async Task<ICollection<PurchaseOrderModel>> GetAllAsync()
        {
            var products = await _session.Query<PurchaseOrderModel>()
                    .Include<PurchaseOrderModel, Client>(order => order.Client.Id)
                    .Include<PurchaseOrderModel, Product>(order => order.Product.Id)
                    .ToListAsync();

            return products;
        }

        public async Task SaveAsync(PurchaseOrder entity)
        {
            var model = (PurchaseOrderModel)entity;

            await _session.StoreAsync(model, model.Id);

            await _session.SaveChangesAsync();
        }

        #region DISPOSE

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _session?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PurchaseOrderRavenDbRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}