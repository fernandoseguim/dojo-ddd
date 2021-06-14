using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Aggregates;
using DojoDDD.Domain.Enums;
using DojoDDD.Infra.DbContext;
using NSpecifications;

namespace DojoDDD.Infra.Repositories
{
    public class OrdemCompraRepositorio : IEntityRepository<PurchaseOrder>
    {
        private readonly DataStore _dataStore;

        public OrdemCompraRepositorio(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<bool> AlterarOrdemCompra(PurchaseOrder ordemCompra)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AlterarOrdemCompra(string ordemId, OrdemCompraStatus novoOrdemCompraStatus)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ConsultarPorId(string id)
        {
            var ordemCompra = await Task.FromResult(_dataStore.OrdensCompras.Find(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))).ConfigureAwait(false);
            return ordemCompra.Id;
        }

        public async Task<string> RegistrarOrdemCompra(PurchaseOrder ordemCompra)
        {
            await Task.Run(() => _dataStore.OrdensCompras.Add(ordemCompra)).ConfigureAwait(false);
            return ordemCompra.Id;
        }

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