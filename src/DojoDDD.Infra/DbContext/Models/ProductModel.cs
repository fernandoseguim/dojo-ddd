using System;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Products.Entities;

namespace DojoDDD.Infra.DbContext.Models
{
    public class ProductModel : IDataStoreModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PurchaseMinAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static explicit operator Product(ProductModel model)
        {
            if(model is null) return null;
            return new Product(IdHelper.RemoveProductsPrefix(model.Id), model.Description, model.AvailableQuantity, model.UnitPrice, model.PurchaseMinAmount, model.CreatedAt, model.UpdatedAt);
        }

        public static implicit operator ProductModel(Product entity)
            => entity is null ? null : new ProductModel
            {
                    Id = IdHelper.LoadForProducts(entity.Id),
                    Description = entity.Description,
                    AvailableQuantity = entity.AvailableQuantity,
                    UnitPrice = entity.UnitPrice,
                    PurchaseMinAmount = entity.PurchaseMinAmount,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
            };
    }
}