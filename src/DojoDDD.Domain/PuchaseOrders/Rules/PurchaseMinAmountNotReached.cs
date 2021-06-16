using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Errors;

namespace DojoDDD.Domain.PuchaseOrders.Rules
{
    public class PurchaseMinAmountNotReached : IRule<PurchaseOrder, DetailedError>
    {
        public string Name => nameof(PurchaseMinAmountNotReached);

        public Task<DetailedError> ApplyFrom(PurchaseOrder instance)
            => instance.OrderAmount < instance.Product.PurchaseMinAmount
                ? Task.FromResult<DetailedError>(new PurchaseMinAmountNotReachedError(instance.Product.PurchaseMinAmount, instance.OrderAmount))
                : Task.FromResult<DetailedError>(default);
    }
}