using System.Threading.Tasks;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Handlers;
using DojoDDD.UnitTests.Fixtures;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using Xunit;

namespace DojoDDD.UnitTests.Handlers
{
    public class PurchaseOrderRegisterCommandHandlerTests : IClassFixture<ServiceTestFixture>
    {
        private readonly ServiceTestFixture _fixture;
        private readonly PurchaseOrderRegisterCommandHandler _sut;

        public PurchaseOrderRegisterCommandHandlerTests(ServiceTestFixture fixture)
        {
            _fixture = fixture;
            _sut = new PurchaseOrderRegisterCommandHandler(_fixture.ClientsRepository, _fixture.ProductsRepository, _fixture.OrderRepository, _fixture.RulesForRegisterNewPurchaseOrder);
        }

        [Theory(DisplayName = "Dado que o comando esteja válido, Quando processar o comando, Então deve retornar result Success")]
        [AutoNSubstituteData]
        public async Task ShouldReturnResultSuccess(PurchaseOrderRegisterCommand command, Client client, Product product, PurchaseOrder order)
        {
            _fixture.ClientsRepository.GetAsync(Arg.Any<string>()).ReturnsForAnyArgs(client);
            _fixture.ProductsRepository.GetAsync(Arg.Any<string>()).ReturnsForAnyArgs(product);
            _fixture.RulesForRegisterNewPurchaseOrder.ApplyRules(order).ReturnsForAnyArgs(Result.Ok(order));

            var result = await _sut.HandleAsync(command);

            result.IsSuccess.Should().BeTrue();
        }

        [Theory(DisplayName = "Dado que o comando esteja inválido, Quando processar o comando, Então deve retornar result Fail")]
        [AutoNSubstituteData]
        public async Task ShouldReturnResultFail(PurchaseOrderRegisterCommand command, Client client, Product product, PurchaseOrder order)
        {
            _fixture.ClientsRepository.GetAsync(Arg.Any<string>()).ReturnsForAnyArgs(client);
            _fixture.ProductsRepository.GetAsync(Arg.Any<string>()).ReturnsForAnyArgs(product);
            _fixture.RulesForRegisterNewPurchaseOrder.ApplyRules(order).ReturnsForAnyArgs(Result.Fail<PurchaseOrder>(new Error("some error")));

            var result = await _sut.HandleAsync(command);

            result.IsFailed.Should().BeTrue();
        }
    }
}