using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Enums;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DojoDDD.UnitTests.Domain.Entities
{
    public class PurchaseOrderTests
    {
        [Theory(DisplayName = "Quando criar uma ordem de compra, Então deve calcular o valor do pedido")]
        [AutoInlineData(10)]
        public void ShouldCalculateOrderAmount(int requestedQuantity, Client client, Product product)
        {
            var expected = product.UnitPrice * requestedQuantity;
            var order = PurchaseOrder.Create(client, product, requestedQuantity);

            order.OrderAmount.Should().Be(expected);
        }

        [Theory(DisplayName = "Quando criar uma ordem de compra, Então deve definir o status como Requested")]
        [AutoInlineData(10)]
        public void ShouldSetStatusAsRequested(int requestedQuantity, Client client, Product product)
        {
            var order = PurchaseOrder.Create(client, product, requestedQuantity);

            order.Status.Should().Be(OrderStatus.Requested);
        }
    }
}