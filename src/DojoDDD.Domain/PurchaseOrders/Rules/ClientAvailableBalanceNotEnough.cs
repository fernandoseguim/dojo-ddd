using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Errors;

namespace DojoDDD.Domain.PurchaseOrders.Rules
{
    public class ClientAvailableBalanceNotEnough : IRule<PurchaseOrder, DetailedError>
    {
        public string Name => nameof(ClientAvailableBalanceNotEnough);

        public Task<DetailedError> ApplyFrom(PurchaseOrder instance)
            => instance.OrderAmount > instance.Client.Balance
                ? Task.FromResult<DetailedError>(new ClientAvailableBalanceNotEnoughError(instance.Client.Balance))
                : Task.FromResult<DetailedError>(default);
    }
}