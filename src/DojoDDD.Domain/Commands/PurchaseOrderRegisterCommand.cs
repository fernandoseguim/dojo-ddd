using DojoDDD.Domain.Abstractions.Commands;
using FluentValidation;

namespace DojoDDD.Domain.Commands
{
    public class PurchaseOrderRegisterCommand : Command
    {
        public PurchaseOrderRegisterCommand(string clientId, int productId, int requestedQuantity)
        {
            ClientId = clientId;
            ProductId = productId;
            RequestedQuantity = requestedQuantity;
            Validate(this, new InlineValidator<PurchaseOrderRegisterCommand>
            {
                    rules => rules.RuleFor(command => command.ClientId).NotEmpty(),
                    rules => rules.RuleFor(command => command.ProductId).NotEmpty().GreaterThan(0),
                    rules => rules.RuleFor(command => command.RequestedQuantity).NotEmpty().GreaterThan(0)
            });
        }

        public string ClientId { get; }
        public int ProductId { get; }
        public int RequestedQuantity { get; }
    }
}