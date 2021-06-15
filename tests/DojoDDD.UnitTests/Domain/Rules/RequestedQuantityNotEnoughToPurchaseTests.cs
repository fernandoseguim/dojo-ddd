using System.Threading.Tasks;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Errors;
using DojoDDD.Domain.Rules;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DojoDDD.UnitTests.Domain.Rules
{
    public class RequestedQuantityNotEnoughToPurchaseTests
    {
        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade solicita seja menor ou igual a zero, Então deve retornar erro QUANTIDADE_SOLICITADA_NAO_SUFICIENTE")]
        [AutoNSubstituteData]
        public async Task ShouldReturnError(Client client, Product product, RequestedQuantityNotEnoughToPurchase sut)
        {
            var order = PurchaseOrder.Create(client, product, 0);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeEquivalentTo(new RequestedQuantityNotEnoughError(order.RequestedQuantity));
        }

        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade solicita seja superior a zero, Então não deve retornar erro")]
        [AutoNSubstituteData]
        public async Task ShouldNotReturnError(Client client, Product product, RequestedQuantityNotEnoughToPurchase sut)
        {
            var order = PurchaseOrder.Create(client, product, 1);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeNull();
        }
    }
}