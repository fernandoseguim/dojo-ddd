using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Errors;

namespace DojoDDD.Domain.PuchaseOrders.Rules
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