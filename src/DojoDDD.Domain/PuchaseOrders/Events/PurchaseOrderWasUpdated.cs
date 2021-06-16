using System;
using DojoDDD.Domain.PuchaseOrders.Entities;

namespace DojoDDD.Domain.PuchaseOrders.Events
{
    public class PurchaseOrderWasUpdated : IEvent<PurchaseOrder>
    {
        public PurchaseOrderWasUpdated(PurchaseOrder data)
        {
            EntityId = data.Id;
            Timestamp = data.CreatedAt;
            Data = data;
        }

        public string EntityId { get; }


        public virtual string Name => nameof(PurchaseOrderWasRequested);

        public DateTime Timestamp { get; }

        public PurchaseOrder Data { get; }
    }
}