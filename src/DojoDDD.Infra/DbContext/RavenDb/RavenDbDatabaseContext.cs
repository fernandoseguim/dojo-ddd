using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public class RavenDbDatabaseContext : IDatabaseContext<IDocumentStore>
    {
        private readonly DbContextOptions _options;

        public RavenDbDatabaseContext(IDocumentStore store, IOptions<DbContextOptions> options)
        {
            if(options is null) throw new ArgumentNullException(nameof(options));

            Store = store;
            _options = options.Value;
        }

        public IDocumentStore Store { get; }

        public async Task ConfigureAsync()
        {
            var databases = _options.Databases;

            foreach(var database in databases)
            {
                if(await Exist(database))
                    continue;

                await CreateAsync(database);
            }
        }

        public async Task<bool> Exist(string database)
        {
            if(string.IsNullOrWhiteSpace(database)) throw new ArgumentNullException(nameof(database));

            var operation = new GetDatabaseNamesOperation(0, 9999);
            var databases = await Store.Maintenance.Server.SendAsync(operation);

            return databases.Any(database.Equals);
        }

        private async Task CreateAsync(string database)
        {
            var record = new DatabaseRecord(database);
            var operation = new CreateDatabaseOperation(record);
            await Store.Maintenance.Server.SendAsync(operation);
        }
    }

    public class RavenDbCelebrityEventStoreContext : IDatabaseContext<IDocumentStore>
    {
        private readonly DbContextOptions _options;

        public RavenDbCelebrityEventStoreContext(IDocumentStore store, IOptions<DbContextOptions> options)
        {

            if(options is null) throw new ArgumentNullException(nameof(options));

            Store = store;
            _options = options.Value;
        }

        public IDocumentStore Store { get; }

        public async Task ConfigureAsync()
        {
            if(await Exist(_options.EventStore))
                return;

            await CreateAsync(_options.EventStore);
        }

        public async Task<bool> Exist(string database)
        {
            if(string.IsNullOrWhiteSpace(database)) throw new ArgumentNullException(nameof(database));

            var operation = new GetDatabaseNamesOperation(0, 9999);
            var databases = await Store.Maintenance.Server.SendAsync(operation);

            return databases.Any(database.Equals);
        }

        private async Task CreateAsync(string database)
        {
            var record = new DatabaseRecord(database);
            var operation = new CreateDatabaseOperation(record);
            await Store.Maintenance.Server.SendAsync(operation);
        }
    }
}
