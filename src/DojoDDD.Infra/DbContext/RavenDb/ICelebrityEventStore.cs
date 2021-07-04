using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Persistence;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public interface ICelebrityEventStore<TCelebrity, TEventStoreModel>
            where TCelebrity : CelebrityAggregateBase
            where TEventStoreModel : IEventStoreModel<object>, new()
    {
        public Task CommitAsync(TCelebrity celebrity, CancellationToken cancellationToken);

        public Task<TCelebrity> LoadAsync(string aggregateId, CancellationToken cancellationToken);

        Task<IEnumerable<TEventStoreModel>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken);
    }
}