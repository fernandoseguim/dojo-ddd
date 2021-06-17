using System;
using DojoDDD.Domain.Abstractions.Commands;
using DojoDDD.Domain.ValueObjects;
using Newtonsoft.Json;

namespace DojoDDD.Domain.PuchaseOrders.Commands
{
    public class PurchaseOrderProcessingCommand : Command
    {
        public PurchaseOrderProcessingCommand(string orderId)
            => OrderId = orderId;

        [JsonConstructor]
        public PurchaseOrderProcessingCommand(string orderId, Scheduling scheduling)
        {
            OrderId = orderId;
            Scheduling = scheduling;
        }

        public string OrderId { get; }

        public Scheduling Scheduling { get; }
    }
}