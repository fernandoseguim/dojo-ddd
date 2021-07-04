using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Persistence;
using DojoDDD.Domain.Abstractions.Entities;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public class RavenDbCelebrityEventStore<TCelebrity, TEventStoreModel> : ICelebrityEventStore<TCelebrity, TEventStoreModel>
            where TCelebrity : CelebrityAggregateBase
            where TEventStoreModel : IEventStoreModel<object>, new()
    {
        private readonly IAsyncDocumentSession _session;
        private readonly ICelebrityEventFactory<object> _eventFactory;

        public RavenDbCelebrityEventStore(IDocumentStore store, IOptions<DbContextOptions> options, ICelebrityEventFactory<object> eventFactory)
        {
            //store.Conventions.FindClrTypeName = _ => nameof(TEventStoreModel);
            _session = store.OpenAsyncSession(options.Value.EventStore);
            _eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
        }

        public virtual async Task CommitAsync(TCelebrity celebrity, CancellationToken cancellationToken)
        {
            if(celebrity is null) throw new ArgumentNullException(nameof(celebrity));

            if(!celebrity.HasUncommittedEvents)
                return;

            var model = await ReadEventsAsync(celebrity.AggregateId, cancellationToken);
            var version = GetStoredVersionOf(model);

            if(version != (celebrity.Version - celebrity.GetUncommittedEvents().Count))
                throw new InvalidOperationException("Invalid object state.");

            // model ??= new TEventStoreModel
            // {
            //         AggregateId = celebrity.AggregateId,
            //         AggregateVersion = celebrity.Version
            // };

            model.Events.AddRange(celebrity.GetUncommittedEvents());

            await _session.StoreAsync(model, cancellationToken);

            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TCelebrity> LoadAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var model = await ReadEventsAsync(aggregateId, cancellationToken);
            model.AggregateId = IdHelper.RemoveOrdersPrefix(model.AggregateId);
            return model.Events.TakeSnapshot<TCelebrity>();
        }

        public virtual async Task<IEnumerable<TEventStoreModel>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken) => await _session
                .Query<TEventStoreModel>()
                .Where(entity => entity.AggregateId == aggregateId).ToListAsync(cancellationToken);

        private long GetStoredVersionOf(TEventStoreModel model)
        {
            if(model is null) return 0;
            return _session.Advanced.GetMetadataFor(model).TryGetValue("EntityVersion", out var value) ? long.Parse(value) : 0;
        }

        private CelebrityEventBase ParseToCelebrityEventBase(TEventStoreModel document)
        {
            var @event = _eventFactory.GetEvent(document.Name, document.Data);

            @event.SetAggregateId(document.AggregateId);
            @event.SetAggregateVersion(document.AggregateVersion);
            @event.SetTimestamp(document.Timestamp);

            return @event;
        }
    }
}