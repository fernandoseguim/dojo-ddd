using AutoFixture;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Rules.RuleBooks;
using NSubstitute;

namespace DojoDDD.UnitTests.Fixtures
{
    public class ServiceTestFixture
    {
        public ServiceTestFixture()
        {
            var fixture = FixtureFactory.Create();
            ClientsRepository = fixture.Create<IEntityRepository<Client>>();
            ProductsRepository = fixture.Create<IEntityRepository<Product>>();
            OrderRepository = fixture.Create<IEntityRepository<PurchaseOrder>>();
            RulesForRegisterNewPurchaseOrder = Substitute.For<RulesForRegisterNewPurchaseOrder>();
        }

        public IEntityRepository<Client> ClientsRepository { get; }
        public IEntityRepository<Product> ProductsRepository { get; }
        public IEntityRepository<PurchaseOrder> OrderRepository { get; }
        public RulesForRegisterNewPurchaseOrder RulesForRegisterNewPurchaseOrder { get; }
    }
}