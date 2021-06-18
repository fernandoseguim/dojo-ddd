using System.Threading.Tasks;
using AspNetCore.AsyncInitialization;
using Raven.Client.Documents;

namespace DojoDDD.Infra.DbContext
{
    public class DatabaseContextInitializer : IAsyncInitializer
    {
        private readonly IDatabaseContext<IDocumentStore> _databaseContext;

        public DatabaseContextInitializer(IDatabaseContext<IDocumentStore> databaseContext)
            => _databaseContext = databaseContext;

        public async Task InitializeAsync()
            => await _databaseContext.ConfigureAsync();
    }
}
