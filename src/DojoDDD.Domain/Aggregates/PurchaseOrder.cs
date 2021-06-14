using System;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Enums;

namespace DojoDDD.Domain.Aggregates
{
    public class PurchaseOrder : Entity
    {
        public PurchaseOrder()
        {
            Status = OrdemCompraStatus.Solicitado;
        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ClientId { get; set; }
        public int OrderedQuantity { get; set; }

        public decimal OperationAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public OrdemCompraStatus Status { get; set; }
    }
}