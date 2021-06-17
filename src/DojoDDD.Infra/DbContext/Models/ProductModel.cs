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

        public static explicit operator Product(ProductModel model)
            => model is null ? null : new Product(int.Parse(model.Id) , model.Description, model.AvailableQuantity, model.UnitPrice, model.PurchaseMinAmount);

        public static implicit operator ProductModel(Product entity)
            => entity is null ? null : new ProductModel
            {
                    Id = $"{entity.Id}",
                    Description = entity.Description,
                    AvailableQuantity = entity.AvailableQuantity,
                    UnitPrice = entity.UnitPrice,
                    PurchaseMinAmount = entity.PurchaseMinAmount
            };
    }
}