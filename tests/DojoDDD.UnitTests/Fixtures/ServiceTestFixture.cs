using AutoFixture;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Rules.RuleBooks;
using NSubstitute;

namespace DojoDDD.UnitTests.Fixtures
{
    public class ServiceTestFixture
    {
        public ServiceTestFixture()
        {
            var fixture = FixtureFactory.Create();
            ClientsRepository = fixture.Create<IQueryableRepository<Client>>();
            ProductsRepository = fixture.Create<IQueryableRepository<Product>>();
            OrderRepository = fixture.Create<IEntityRepository<PurchaseOrder>>();
            RulesForRegisterNewPurchaseOrder = Substitute.For<RulesForRegisterNewPurchaseOrder>();
        }

        public IQueryableRepository<Client> ClientsRepository { get; }
        public IQueryableRepository<Product> ProductsRepository { get; }
        public IEntityRepository<PurchaseOrder> OrderRepository { get; }
        public RulesForRegisterNewPurchaseOrder RulesForRegisterNewPurchaseOrder { get; }
    }
}