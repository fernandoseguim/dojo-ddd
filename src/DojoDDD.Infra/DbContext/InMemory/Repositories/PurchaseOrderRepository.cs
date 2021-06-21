using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Infra.DbContext.InMemory.Repositories
{
    public class PurchaseOrderInMemoryRepository : IEntityRepository<PurchaseOrder>, IQueryableRepository<PurchaseOrderModel>
    {
        private readonly DataStore _dataStore;

        public PurchaseOrderInMemoryRepository(DataStore dataStore) => _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

        public Task<PurchaseOrder> GetAsync(string id)
        {
            var order = _dataStore.OrdensCompras.FirstOrDefault(c => id.Equals(c.Id));

            return Task.FromResult((PurchaseOrder) order);
        }

        public async Task<PurchaseOrderModel> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            var orders = await GetManyAsync(spec);
            return orders.FirstOrDefault();
        }

        public async Task<ICollection<PurchaseOrderModel>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<PurchaseOrderModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var expression = spec.Expression.Compile();
            var clientes = _dataStore.OrdensCompras.Where(expression).ToList();
            return await Task.FromResult(clientes).ConfigureAwait(false);
        }

        public Task<ICollection<PurchaseOrderModel>> GetAllAsync()
        {
            var orders = (ICollection<PurchaseOrderModel>) _dataStore.OrdensCompras.Select(model => (PurchaseOrderModel) model).ToList();

            return Task.FromResult(orders);
        }

        public Task SaveAsync(PurchaseOrder entity)
        {
            _dataStore.OrdensCompras.Add(entity);

            return Task.CompletedTask;
        }
    }
}
