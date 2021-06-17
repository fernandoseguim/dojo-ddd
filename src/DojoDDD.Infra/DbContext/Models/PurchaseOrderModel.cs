using System;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Enums;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Infra.DbContext.Models
{
    public class PurchaseOrderInMemoryModel : PurchaseOrderQueryModel
    {
        public static explicit operator PurchaseOrder(PurchaseOrderInMemoryModel model)
            => model is null ? null : new PurchaseOrder(model.Id, (Product) model.Product, (Client) model.Client, model.RequestedQuantity, model.OrderAmount, model.Status, model.Scheduling);

        public static implicit operator PurchaseOrderInMemoryModel(PurchaseOrder entity)
            => entity is null ? null : new PurchaseOrderInMemoryModel
            {
                    Id = entity.Id,
                    Product = entity.Product,
                    Client = entity.Client,
                    RequestedQuantity = entity.RequestedQuantity,
                    OrderAmount = entity.OrderAmount,
                    Status = entity.Status,
                    Scheduling = entity.Scheduling
            };
    }

    public class PurchaseOrderModel : IDataStoreModel
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ProductId { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal OrderAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Scheduling Scheduling { get; set; }

        public static implicit operator PurchaseOrderModel(PurchaseOrder entity)
            => entity is null ? null : new PurchaseOrderModel
            {
                    Id = entity.Id,
                    ProductId = entity.Product.Id.ToString(),
                    ClientId = entity.Client.Id,
                    RequestedQuantity = entity.RequestedQuantity,
                    OrderAmount = entity.OrderAmount,
                    Status = entity.Status,
                    Scheduling = entity.Scheduling
            };
    }

    public class PurchaseOrderQueryModel : IDataStoreModel
    {
        public string Id { get; set; }
        public ClientModel Client { get; set; }
        public ProductModel Product { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal OrderAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Scheduling Scheduling { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static explicit operator PurchaseOrder(PurchaseOrderQueryModel model)
            => model is null ? null : new PurchaseOrder(model.Id, (Product) model.Product, (Client) model.Client, model.RequestedQuantity, model.OrderAmount, model.Status, model.Scheduling);

        // public static implicit operator PurchaseOrderModelB(PurchaseOrder entity)
        //     => entity is null ? null : new PurchaseOrderModelB
        //     {
        //             Id = entity.Id,
        //             Product = entity.Product,
        //             Client = entity.Client,
        //             RequestedQuantity = entity.RequestedQuantity,
        //             OrderAmount = entity.OrderAmount,
        //             Status = entity.Status,
        //             ScheduledTo = entity.ScheduledTo
        //     };
    }
}