using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Enums;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.PurchaseOrders.Entities
{
    public class PurchaseOrder : Entity
    {
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

            return order;
        }

        public async Task Schedule(Func<Task<Scheduling>> scheduler)
        {
            UpdatedAt = DateTime.UtcNow;
            Status = OrderStatus.Scheduled;
            Scheduling = await scheduler();
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
    }
}