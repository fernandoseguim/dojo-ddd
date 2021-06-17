using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.PuchaseOrders.Entities;

namespace DojoDDD.Infra.DbContext.InMemory.Repositories
{
    public class PurchaseOrderInMemoryRepository : IEntityRepository<PurchaseOrder>
    {
        private readonly DataStore _dataStore;

        public PurchaseOrderInMemoryRepository(DataStore dataStore) => _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

        public Task<PurchaseOrder> GetAsync(string id)
        {
            var client = _dataStore.OrdensCompras.FirstOrDefault(c => id.Equals(c.Id));

            return Task.FromResult(client);
        }

        public async Task<PurchaseOrder> GetAsync<TSpec>(TSpec spec) where TSpec : Specification<PurchaseOrder>
        {
            var orders = await GetManyAsync(spec);
            return orders.FirstOrDefault();
        }

        public async Task<ICollection<PurchaseOrder>> GetManyAsync<TSpec>(TSpec spec) where TSpec : Specification<PurchaseOrder>
        {
            if(spec is null)
                return await Task.FromResult(_dataStore.OrdensCompras).ConfigureAwait(false);

            var expression = spec.Expression.Compile();
            var clientes = _dataStore.OrdensCompras.Where(expression).ToList();
            return await Task.FromResult(clientes).ConfigureAwait(false);
        }

        public Task<ICollection<PurchaseOrder>> GetAllAsync() => throw new NotImplementedException();

        public Task SaveAsync(PurchaseOrder entity)
        {
            _dataStore.OrdensCompras.Add(entity);

            return Task.CompletedTask;
        }
    }
}
