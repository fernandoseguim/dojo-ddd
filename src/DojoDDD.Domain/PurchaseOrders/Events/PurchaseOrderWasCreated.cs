using System;
using Amaury.Abstractions;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Enums;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.PurchaseOrders.Events
{
    public class PurchaseOrderWasCreated : CelebrityEventBase
    {
        public PurchaseOrderWasCreated(string id, Product product,
                                       Client client,
                                       int requestedQuantity,
                                       decimal orderAmount,
                                       OrderStatus status,
                                       DateTime createdAt)
        {
            AggregateId = id;
            Product = product;
            Client = client;
            RequestedQuantity = requestedQuantity;
            OrderAmount = orderAmount;
            Status = status;
            CreatedAt = createdAt;
        }

        public override string Name => nameof(PurchaseOrderWasCreated);

        public Product Product { get; }
        public Client Client { get; }
        public int RequestedQuantity { get; }
        public decimal OrderAmount { get; }
        public OrderStatus Status { get; }
        public DateTime CreatedAt { get; }
    }

    public class PurchaseOrderWasScheduled : CelebrityEventBase
    {
        public PurchaseOrderWasScheduled(string id, Scheduling scheduling, OrderStatus status, DateTime? updatedAt)
        {
            AggregateId = id;
            Scheduling = scheduling;
            Status = status;
            UpdatedAt = updatedAt;
        }

        public override string Name => nameof(PurchaseOrderWasScheduled);

        public Scheduling Scheduling { get; }
        public OrderStatus Status { get; }
        public DateTime? UpdatedAt { get; }
    }
}