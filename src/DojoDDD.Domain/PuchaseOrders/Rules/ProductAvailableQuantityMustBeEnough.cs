using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Errors;

namespace DojoDDD.Domain.PuchaseOrders.Rules
{
    public class ProductAvailableQuantityMustBeEnough : IRule<PurchaseOrder, DetailedError>
    {
        public string Name => nameof(ProductAvailableQuantityMustBeEnough);

        public Task<DetailedError> ApplyFrom(PurchaseOrder instance)
        {
            if(instance.Product.AvailableQuantity <= 0)
                return Task.FromResult<DetailedError>(new ProductUnavailableError());

            return instance.RequestedQuantity > instance.Product.AvailableQuantity
                    ? Task.FromResult<DetailedError>(new ProductQuantityNotEnoughError(instance.Product.AvailableQuantity, instance.RequestedQuantity))
                    : Task.FromResult<DetailedError>(default);
        }
    }
}