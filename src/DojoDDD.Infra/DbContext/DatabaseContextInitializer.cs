using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.AsyncInitialization;
using Raven.Client.Documents;

namespace DojoDDD.Infra.DbContext
{
    public class DatabaseContextInitializer : IAsyncInitializer
    {
        private readonly IEnumerable<IDatabaseContext<IDocumentStore>> _databaseContexts;

        public DatabaseContextInitializer(IEnumerable<IDatabaseContext<IDocumentStore>> databaseContexts)
            => _databaseContexts = databaseContexts;

        public async Task InitializeAsync()
        {
            foreach(var databaseContext in _databaseContexts)
            {
                 await databaseContext.ConfigureAsync();
            }
        }
    }
}
