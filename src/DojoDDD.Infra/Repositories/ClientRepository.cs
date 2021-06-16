using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Infra.DbContext;
using NSpecifications;

namespace DojoDDD.Infra.Repositories
{
    public class ClientRepository : IQueryableRepository<Client>
    {
        private readonly DataStore _dataStore;

        public ClientRepository(DataStore dataStore) => _dataStore = dataStore;

        public async Task<Client> GetAsync<TSpec>(TSpec spec) where TSpec : ASpec<Client>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var clientes = await GetManyAsync(spec);

            return clientes.FirstOrDefault();
        }

        public async Task<ICollection<Client>> GetManyAsync<TSpec>(TSpec spec) where TSpec : ASpec<Client>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var expression = spec.Expression.Compile();
            var clientes = _dataStore.Clientes.Where(expression).ToList();
            return await Task.FromResult(clientes).ConfigureAwait(false);
        }

        public async Task<ICollection<Client>> GetAllAsync() => await Task.FromResult(_dataStore.Clientes).ConfigureAwait(false);
    }
}