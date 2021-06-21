﻿using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Bogus;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Rules.RuleBooks;
using DojoDDD.Domain.ValueObjects;
using DojoDDD.Infra.DbContext.InMemory;
using DojoDDD.Infra.DbContext.Models;
using NSubstitute;

namespace DojoDDD.UnitTests.Fixtures
{
    public static class FixtureFactory
    {
        public static IFixture Create()
        {
            var faker = new Faker("pt_BR");

            var fixture = new Fixture()
                    .Customize(new SubstituteCustomization())
                    .Customize(new DataStoreCustomization())
                    .Customize(new EntitiesCustomization(faker))
                    .Customize(new CommandsCustomization(faker))
                    .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 1;

            return fixture;
        }

        internal class SubstituteCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() => Substitute.For<IQueryableRepository<ClientModel>>());
                fixture.Register(() => Substitute.For<IQueryableRepository<ProductModel>>());
                fixture.Register(() => Substitute.For<IQueryableRepository<PurchaseOrderModel>>());

                fixture.Register(() => Substitute.For<IEntityRepository<Client>>());
                fixture.Register(() => Substitute.For<IEntityRepository<Product>>());
                fixture.Register(() => Substitute.For<IEntityRepository<PurchaseOrder>>());
                fixture.Register(() => new RulesForRegisterNewPurchaseOrder());
            }
        }

        internal class DataStoreCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() => new DataStore());
            }
        }

        internal class EntitiesCustomization : ICustomization
        {
            private readonly Faker _faker;

            public EntitiesCustomization(Faker faker) => _faker = faker;

            public void Customize(IFixture fixture)
            {
                fixture.Register(() => Product.Create(_faker.UniqueIndex.ToString(), _faker.Commerce.ProductName(), 1000, decimal.Parse(_faker.Commerce.Price(1, 100, 2)), 500.00M));

                fixture.Register(() => Client.Create(
                        _faker.Name.FullName(),
                        new Address(
                                _faker.Address.ZipCode().Replace("-", string.Empty).PadRight(8, '0'),
                                _faker.Address.StreetName(),
                                _faker.Address.BuildingNumber(),
                                _faker.Random.Word(), _faker.Address.City(),
                                _faker.Address.StateAbbr(),
                                _faker.Address.Country(),
                                _faker.Address.SecondaryAddress()),
                        _faker.Random.Int(18, 75),
                        _faker.Finance.Amount(100, 1000)));
            }
        }

        internal class CommandsCustomization : ICustomization
        {
            private readonly Faker _faker;

            public CommandsCustomization(Faker faker) => _faker = faker;

            public void Customize(IFixture fixture)
            {
                fixture.Register(() => new PurchaseOrderRegisterCommand(_faker.Random.Guid().ToString("N"), _faker.UniqueIndex, _faker.Random.Int(100, 500)));
            }
        }
    }
}