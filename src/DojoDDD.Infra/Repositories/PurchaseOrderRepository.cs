using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Infra.DbContext;
using NSpecifications;

namespace DojoDDD.Infra.Repositories
{
    public class PurchaseOrderRepository : IEntityRepository<PurchaseOrder>
    {
        private readonly DataStore _dataStore;

        public PurchaseOrderRepository(DataStore dataStore) => _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

        public async Task<PurchaseOrder> GetAsync<TSpec>(TSpec spec) where TSpec : ASpec<PurchaseOrder>
        {
            var orders = await GetManyAsync(spec);
            return orders.FirstOrDefault();
        }

        public async Task<ICollection<PurchaseOrder>> GetManyAsync<TSpec>(TSpec spec) where TSpec : ASpec<PurchaseOrder>
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