using System.Threading.Tasks;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Errors;
using DojoDDD.Domain.PurchaseOrders.Rules;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DojoDDD.UnitTests.Domain.Rules
{
    public class PurchaseMinAmountNotReachedTests
    {
        [Theory(DisplayName = "Quando aplicar a regra, Dado que o valor minimo de compra não seja atingido, Então deve retornar erro VALOR_MINIMO_NAO_ATINGIDO")]
        [AutoNSubstituteData]
        public async Task ShouldReturnError(Client client, Product product, PurchaseMinAmountNotReached sut)
        {
            var requestedQuantity = (int)(product.PurchaseMinAmount / product.UnitPrice) - 1;
            var order = PurchaseOrder.Create(client, product, requestedQuantity);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeEquivalentTo(new PurchaseMinAmountNotReachedError(product.PurchaseMinAmount, order.OrderAmount));
        }

        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade solicita atinja o valor minimo de compra, Então não deve retornar erro")]
        [AutoNSubstituteData]
        public async Task ShouldNotReturnError(Client client, Product product, PurchaseMinAmountNotReached sut)
        {
            var requestedQuantity = (int)(product.PurchaseMinAmount / product.UnitPrice) + 1;
            var order = PurchaseOrder.Create(client, product, requestedQuantity);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeNull();
        }
    }
}