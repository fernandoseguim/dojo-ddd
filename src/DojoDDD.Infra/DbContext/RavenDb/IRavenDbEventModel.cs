using System;
using Amaury.Abstractions;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public interface IRavenDbEventModel
    {
        Guid Id { get; set; }
        string AggregateId { get; set; }
        long AggregateVersion { get; set; }
        DateTime Timestamp { get; set; }
        string Name { get; set; }
        object Data { get; set; }
    }

    public class RavenDbEventModel<TEvent> where TEvent : CelebrityEventBase
    {
        public RavenDbEventModel(TEvent @event)
        {
            AggregateId = @event.AggregateId;
            AggregateVersion = @event.AggregateVersion;
            Timestamp = @event.Timestamp;
            Name = @event.Name;
            Data = @event;
        }

        public Guid Id { get; set; }

        public string AggregateId { get; set; }

        public long AggregateVersion { get; set; }

        public DateTime Timestamp { get; set; }

        public string Name { get; set; }

        public TEvent Data { get; set; }
    }
}