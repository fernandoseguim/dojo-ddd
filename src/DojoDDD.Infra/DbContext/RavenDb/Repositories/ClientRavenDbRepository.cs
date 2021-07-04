using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DojoDDD.Infra.DbContext.RavenDb.Repositories
{
    public class ClientRavenDbRepository : IEntityRepository<Client>, IQueryableRepository<ClientModel>, IDisposable
    {
        private readonly IAsyncDocumentSession _session;

        public ClientRavenDbRepository(IDatabaseContext<IDocumentStore> context, IOptions<DbContextOptions> options)
        {
            if(options is null) throw new ArgumentNullException(nameof(options));

            var database = options.Value.Databases.FirstOrDefault();

            if(database is null) throw new InvalidOperationException();

            _session = context.Store.OpenAsyncSession(database);
        }

        public async Task<Client> GetAsync(string id)
        {
            var client = await _session.LoadAsync<ClientModel>(IdHelper.LoadForClients(id));
            return (Client) client;
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

            var clientes = await _session.Query<ClientModel>().Where(spec.Expression).ToListAsync();

            return clientes;
        }

        public async Task<ICollection<ClientModel>> GetAllAsync()
        {
            var clientes = await _session.Query<ClientModel>().ToListAsync();

            return clientes;
        }

        public async Task SaveAsync(Client entity)
        {
            var model = (ClientModel)entity;
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

        ~ClientRavenDbRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}