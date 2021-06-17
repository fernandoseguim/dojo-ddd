using System;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Api.Controllers.v1.Models
{
    public class ProductLegacy
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Estoque { get; set; }
        public string PrecoUnitario { get; set; }
        public int ValorMinimoDeCompra { get; set; }

        public static implicit operator ProductLegacy(ProductModel entity)
            => entity is null ? null : new ProductLegacy
            {
                    Id = int.Parse(entity.Id),
                    Descricao = entity.Description,
                    Estoque = entity.AvailableQuantity,
                    PrecoUnitario = entity.UnitPrice.ToString("0.00"),
                    ValorMinimoDeCompra = Convert.ToInt32(entity.PurchaseMinAmount)
            };
    }
}