using System;
using DojoDDD.Domain.Abstractions.Entities;

namespace DojoDDD.Domain.Products.Entities
{
    public class Product : Entity, IEquatable<Product>
    {
        public Product(int id, string description, int availableQuantity, decimal unitPrice, decimal purchaseMinAmount)
        {
            Id = id;
            Description = description;
            AvailableQuantity = availableQuantity;
            UnitPrice = unitPrice;
            PurchaseMinAmount = purchaseMinAmount;
        }

        public int Id { get;}
        public string Description { get;}
        public int AvailableQuantity { get; }
        public decimal UnitPrice { get;}
        public decimal PurchaseMinAmount { get;}

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

        public override int GetHashCode() => Id;

        public static bool operator ==(Product left, Product right) => Equals(left, right);

        public static bool operator !=(Product left, Product right) => !Equals(left, right);
    }
}