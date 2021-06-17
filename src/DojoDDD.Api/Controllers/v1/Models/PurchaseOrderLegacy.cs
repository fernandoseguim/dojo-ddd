using System;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Api.Controllers.v1.Models
{
    public class PurchaseOrderLegacy
    {
        public string Id { get;set; }

        public DateTime DataOperacao { get; set; }
        public int ProdutoId { get; set; }
        public string ClienteId { get; set; }
        public int QuantidadeSolicitada { get; set; }
        public decimal ValorOperacao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public PurchaseOrderStatusLegacy Status { get; set; }

        public static implicit operator PurchaseOrderLegacy(PurchaseOrderQueryModel entity)
            => entity is null ? null : new PurchaseOrderLegacy
            {
                    Id = entity.Id,
                    DataOperacao = entity.CreatedAt,
                    ProdutoId = int.Parse(entity.Product.Id),
                    ClienteId = entity.Client.Id,
                    QuantidadeSolicitada = entity.RequestedQuantity,
                    ValorOperacao = entity.OrderAmount,
                    PrecoUnitario = entity.Product.UnitPrice,
                    Status = (PurchaseOrderStatusLegacy)(int)entity.Status
            };
    }
}