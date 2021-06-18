using DojoDDD.Domain.Abstractions.Commands;

namespace DojoDDD.Domain.Products.Commands
{
    public class CreateProductCommand : Command
    {
        public CreateProductCommand(string description, int availableQuantity, decimal unitPrice, decimal purchaseMinAmount)
        {
            Description = description;
            AvailableQuantity = availableQuantity;
            UnitPrice = unitPrice;
            PurchaseMinAmount = purchaseMinAmount;
        }

        public string Description { get;}
        public int AvailableQuantity { get; }
        public decimal UnitPrice { get;}
        public decimal PurchaseMinAmount { get;}
    }
}