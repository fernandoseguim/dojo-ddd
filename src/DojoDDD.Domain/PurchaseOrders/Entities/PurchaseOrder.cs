using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Enums;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.PurchaseOrders.Entities
{
    public class PurchaseOrder : Entity
    {
        public PurchaseOrder(IEnumerable<CelebrityEventBase> events)
        {
            foreach(var @event in events)
            {
                Id = @event.AggregateId;
                ApplyEvent(@event);
            }
        }

        public PurchaseOrder(string id, CelebrityEventBase @event)
        {
            Id = id;
            ApplyEvent(@event);
        }

        public PurchaseOrder(string id, Product product, Client client, int requestedQuantity, decimal orderAmount, OrderStatus status, Scheduling scheduling)
        {
            Id = id;
            Product = product;
            Client = client;
            RequestedQuantity = requestedQuantity;
            OrderAmount = orderAmount;
            Status = status;
            Scheduling = scheduling;
        }

        private PurchaseOrder() { }

        public string Id { get; private set; }

        public Product Product { get; private set; }
        public Client Client { get; private set; }
        public int RequestedQuantity { get; private set; }
        public decimal OrderAmount { get; private set; }
        public OrderStatus Status { get; private set; }
        public Scheduling Scheduling { get; private set; }

        public static PurchaseOrder Create(Client client, Product product, int requestedQuantity)
        {
            var order = new PurchaseOrder
            {
                    Id = Guid.NewGuid().ToString("N"),
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Requested,
                    Client = client,
                    Product = product,
                    RequestedQuantity = requestedQuantity,
                    OrderAmount = product.UnitPrice * requestedQuantity,
            };

            order.AppendEvent(new PurchaseOrderWasCreated(order.Id, order.Product, order.Client, order.RequestedQuantity, order.OrderAmount, order.Status, order.CreatedAt));
            return order;
        }

        public async Task Schedule(Func<Task<Scheduling>> scheduler)
        {
            UpdatedAt = DateTime.UtcNow;
            Status = OrderStatus.Scheduled;
            Scheduling = await scheduler();
            AppendEvent(new PurchaseOrderWasScheduled(Id, Scheduling, Status, UpdatedAt));
        }

        public void Cancel()
        {
            Scheduling = null;
            Status = OrderStatus.Canceled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Close()
        {
            Scheduling = null;
            Status = OrderStatus.Closed;
            UpdatedAt = DateTime.UtcNow;
        }

        public override string GetAggregateId() => Id;

        public void Apply(PurchaseOrderWasCreated @event)
        {
            Id = @event.AggregateId;
            Product = @event.Product;
            Client = @event.Client;
            RequestedQuantity = @event.RequestedQuantity;
            OrderAmount = @event.OrderAmount;
            Status = @event.Status;
            CreatedAt = @event.CreatedAt;
        }

        public void Apply(PurchaseOrderWasScheduled @event)
        {
            Id = @event.AggregateId;
            Scheduling = @event.Scheduling;
            Status = @event.Status;
            UpdatedAt = @event.UpdatedAt;
        }
    }
}