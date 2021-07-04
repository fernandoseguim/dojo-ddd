using System;
using Amaury.Persistence;

namespace DojoDDD.Infra.DbContext.RavenDb
{
    public class RavenDbEventStoreModel : IEventStoreModel<object>
    {
        //public string Id { get;set; }

        public string AggregateId { get; set; }

        public string Name { get; set; }

        public long AggregateVersion { get;set; }

        public DateTime Timestamp { get;set; }

        public object Data { get;set; }
    }
}