using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Errors;

namespace DojoDDD.Domain.PurchaseOrders.Rules
{
    public class RequestedQuantityNotEnoughToPurchase : IRule<PurchaseOrder, DetailedError>
    {
        public string Name => nameof(RequestedQuantityNotEnoughToPurchase);

        public Task<DetailedError> ApplyFrom(PurchaseOrder instance)
            => instance.RequestedQuantity <= 0
                ? Task.FromResult<DetailedError>(new RequestedQuantityNotEnoughError(instance.RequestedQuantity))
                : Task.FromResult<DetailedError>(default);
    }
}