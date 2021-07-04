using System;
using System.Linq;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Enums;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.ValueObjects;
using DojoDDD.Infra.DbContext.Models;
using Raven.Client.Documents.Indexes;

namespace DojoDDD.Infra.DbContext.RavenDb.Indexes
{
    public class PurchaseOrderSnapshotIndex : AbstractMultiMapIndexCreationTask<PurchaseOrder>
    {
        public override string IndexName => "PurchaseOrderEvents/Snapshot";

        public PurchaseOrderSnapshotIndex()
        {
            AddMap<PurchaseOrderWasCreated>(events => events.Where(e => e.Name == nameof(PurchaseOrderWasCreated))
                    .Select(e => new PurchaseOrder(e.AggregateId, e)));

            AddMap<PurchaseOrderWasScheduled>(events => events.Where(e => e.Name == nameof(PurchaseOrderWasScheduled))
                    .Select(e => new PurchaseOrder(e.AggregateId, e)));

            Reduce = inputs => inputs.GroupBy(input => input.Id)
                    .Select(g => new PurchaseOrderModel
                    {
                            Id = g.Key,
                            Client = g.Aggregate((ClientModel)null, (client, order) => order.Client ?? client),
                            Product = g.Aggregate((ProductModel)null, (product, order) => order.Product ?? product),
                            RequestedQuantity = g.Aggregate(0, (quantity, order) => order.RequestedQuantity),
                            OrderAmount = g.Aggregate(0.0M, (amount, order) => order.OrderAmount),
                            Status = g.Aggregate(OrderStatus.Requested, (amount, order) => order.Status),
                            Scheduling = g.Aggregate((Scheduling)null, (scheduling, order) => order.Scheduling ?? scheduling),
                            CreatedAt = g.Aggregate(DateTime.UtcNow, (time, order) => order.CreatedAt),
                            UpdatedAt = g.Aggregate((DateTime?)null, (time, order) => order.UpdatedAt),
                    });
        }
    }
}