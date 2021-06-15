using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Errors;

namespace DojoDDD.Domain.Rules
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