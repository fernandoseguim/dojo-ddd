using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Infra.DbContext.InMemory.Repositories
{
    public class ClientInMemoryRepository : IEntityRepository<Client>, IQueryableRepository<ClientModel>
    {
        private readonly DataStore _dataStore;

        public ClientInMemoryRepository(DataStore dataStore) => _dataStore = dataStore;

        public Task<Client> GetAsync(string id)
        {
            var client = _dataStore.Clientes.FirstOrDefault(c => id.Equals(c.Id));

            return Task.FromResult((Client) client);
        }

        public async Task<ClientModel> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<ClientModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var clientes = await GetManyAsync(spec);

            return clientes.FirstOrDefault();
        }

        public async Task<ICollection<ClientModel>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<ClientModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var expression = spec.Expression.Compile();
            var clientes = _dataStore.Clientes.Where(expression).ToList();
            return await Task.FromResult(clientes).ConfigureAwait(false);
        }

        public async Task<ICollection<ClientModel>> GetAllAsync() => await Task.FromResult(_dataStore.Clientes).ConfigureAwait(false);

        public Task SaveAsync(Client entity) => throw new NotImplementedException();
    }
}
