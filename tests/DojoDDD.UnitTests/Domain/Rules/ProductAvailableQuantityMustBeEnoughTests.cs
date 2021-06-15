using System.Threading.Tasks;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Errors;
using DojoDDD.Domain.Rules;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DojoDDD.UnitTests.Domain.Rules
{
    public class ProductAvailableQuantityMustBeEnoughTests
    {
        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade disponível do produto seja menor ou igual a zero, Então deve retornar erro PRODUTO_NAO_DISPONIVEL")]
        [AutoInlineData(0)]
        [AutoInlineData(-1)]
        public async Task ShouldReturnProductUnavailableError(int availableQuantity, Product product, Client client, ProductAvailableQuantityMustBeEnough sut)
        {
            product = new Product(product.Id, product.Description, availableQuantity, product.UnitPrice, product.PurchaseMinAmount);
            var order = PurchaseOrder.Create(client, product, 1);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeEquivalentTo(new ProductUnavailableError());
        }

        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade disponível do produto seja menor do que a quantidade solicitata, Então deve retornar erro QUANTIDADE_PRODUTO_INSUFICIENTE")]
        [AutoNSubstituteData]
        public async Task ShouldReturnProductQuantityNotEnoughError(Product product, Client client, ProductAvailableQuantityMustBeEnough sut)
        {
            var order = PurchaseOrder.Create(client, product, product.AvailableQuantity + 1);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeEquivalentTo(new ProductQuantityNotEnoughError(product.AvailableQuantity, order.RequestedQuantity));
        }

        [Theory(DisplayName = "Quando aplicar a regra, Dado que a quantidade solicita seja inferior a quantidade disponível, Então não deve retornar erro")]
        [AutoNSubstituteData]
        public async Task ShouldNotReturnError(Client client, Product product, RequestedQuantityNotEnoughToPurchase sut)
        {
            var order = PurchaseOrder.Create(client, product, product.AvailableQuantity - 1);

            var reason = await sut.ApplyFrom(order);

            reason.Should().BeNull();
        }
    }
}