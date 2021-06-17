using System;
using DojoDDD.Domain.Products.Entities;

namespace DojoDDD.Api.Controllers.v1.Models
{
    public class ProductLegacy
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Estoque { get; set; }
        public string PrecoUnitario { get; set; }
        public int ValorMinimoDeCompra { get; set; }

        public static implicit operator ProductLegacy(Product entity)
            => entity is null ? null : new ProductLegacy
            {
                    Id = entity.Id,
                    Descricao = entity.Description,
                    Estoque = entity.AvailableQuantity,
                    PrecoUnitario = entity.UnitPrice.ToString("0.00"),
                    ValorMinimoDeCompra = Convert.ToInt32(entity.PurchaseMinAmount)
            };
    }
}