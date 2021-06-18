using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public class RavenDbDatabaseContext : IDatabaseContext<IDocumentStore>
    {
        private readonly string _database;
        private readonly DbContextOptions _options;

        public RavenDbDatabaseContext(IDocumentStore store, IOptions<DbContextOptions> options)
        {
            if(options is null) throw new ArgumentNullException(nameof(options));

            Store = store;
            _options = options.Value;
            _database = _options.Name;
        }

        public IDocumentStore Store { get; }

        public async Task ConfigureAsync()
        {
            if(await Exist(_database))
                return;

            await CreateAsync(_database);
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
