using System;
using DojoDDD.Domain.Abstractions.Entities;

namespace DojoDDD.Domain.Products.Entities
{
    public class Product : Entity, IEquatable<Product>
    {
        public Product(string id, string description, int availableQuantity, decimal unitPrice, decimal purchaseMinAmount, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Description = description;
            AvailableQuantity = availableQuantity;
            UnitPrice = unitPrice;
            PurchaseMinAmount = purchaseMinAmount;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public string Id { get;}
        public string Description { get;}
        public int AvailableQuantity { get; private set; }
        public decimal UnitPrice { get;}
        public decimal PurchaseMinAmount { get;}

        public static Product Create(string id, string description, int availableQuantity, decimal unitPrice, decimal purchaseMinAmount)
            => new(id, description, availableQuantity, unitPrice, purchaseMinAmount, DateTime.UtcNow, null);

        public void IncreaseAvailableQuantity(int quantity)
        {
            AvailableQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseAvailableQuantity(int quantity)
        {
            AvailableQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode() => (Id != null ? Id.GetHashCode() : 0);

        public static bool operator ==(Product left, Product right) => Equals(left, right);

        public static bool operator !=(Product left, Product right) => !Equals(left, right);
    }
}