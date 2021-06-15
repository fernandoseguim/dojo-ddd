using System;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Enums;

namespace DojoDDD.Domain.Entities
{
    public class PurchaseOrder : Entity
    {
        private PurchaseOrder() { }

        public string Id { get; private set; }

        public Product Product { get; private set; }
        public Client Client { get; private set; }
        public int RequestedQuantity { get; private set; }
        public decimal OrderAmount { get; private set; }
        public OrderStatus Status { get; private set; }

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
    }
}