using System;
using DojoDDD.Domain.Abstractions.Commands;
using Newtonsoft.Json;

namespace DojoDDD.Domain.PuchaseOrders.Commands
{
    public class PurchaseOrderProcessingCommand : Command
    {
        public PurchaseOrderProcessingCommand(string orderId)
            => OrderId = orderId;

        [JsonConstructor]
        public PurchaseOrderProcessingCommand(string orderId, DateTime scheduleTo)
        {
            OrderId = orderId;
            ScheduleTo = scheduleTo;
        }

        public string OrderId { get; }

        public DateTime ScheduleTo { get; }
    }
}