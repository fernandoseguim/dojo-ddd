using System.Threading.Tasks;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Errors;
using DojoDDD.Domain.PuchaseOrders.Rules;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DojoDDD.UnitTests.Domain.Rules
{
    public class ClientAvailableBalanceNotEnoughTests
    {
        [Theory(DisplayName = "Quando aplicar a regra, Dado que o cliente não tenha saldo suficiente, Então deve retornar erro SALDO_DO_CLIENTE_INSUFICIENTE")]
        [AutoNSubstituteData]
        public async Task ShouldReturnError(Client client, Product product, ClientAvailableBalanceNotEnough sut)
        {
            var balance = (product.AvailableQuantity * product.UnitPrice) - 0.01M;
            client.DecreaseBalance(client.Balance);
            client.IncreaseBalance(balance);
            var order = PurchaseOrder.Create(client, product, product.AvailableQuantity);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeEquivalentTo(new ClientAvailableBalanceNotEnoughError(client.Balance));
        }

        [Theory(DisplayName = "Quando aplicar a regra, Dado que o clinte tenha saldo suficiente, Então não deve retornar erro")]
        [AutoNSubstituteData]
        public async Task ShouldNotReturnError(Client client, Product product, ClientAvailableBalanceNotEnough sut)
        {
            var balance = (product.AvailableQuantity * product.UnitPrice) + 0.01M;
            client.DecreaseBalance(client.Balance);
            client.IncreaseBalance(balance);
            var order = PurchaseOrder.Create(client, product, product.AvailableQuantity);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeNull();
        }
    }
}