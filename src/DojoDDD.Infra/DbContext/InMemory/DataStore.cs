using System.Collections.Generic;
using System.Linq;
using Bogus;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.ValueObjects;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Infra.DbContext.InMemory
{
    public class DataStore
    {
        public ICollection<PurchaseOrderInMemoryModel> OrdensCompras { get; set; } = new List<PurchaseOrderInMemoryModel>();

        public ICollection<ClientModel> Clientes { get; set; }
        public ICollection<ProductModel> Produtos { get; set; }

        public DataStore()
        {
            LoadFakeData();
        }

        private void LoadFakeData()
        {
            var clientes = new Faker<Client>()
                    .CustomInstantiator(f =>
                            new Client(
                                    f.Random.Guid().ToString("N"),
                                    f.Name.FullName(),
                                    new Address(
                                            f.Address.ZipCode().Replace("-", string.Empty).PadRight(8, '0'),
                                            f.Address.StreetName(),
                                            f.Address.BuildingNumber(),
                                            f.Random.Word(), f.Address.City(),
                                            f.Address.StateAbbr(),
                                            f.Address.Country(),
                                            f.Address.SecondaryAddress()),
                                            f.Random.Int(18, 75),
                                            f.Finance.Amount(1000, 10000)))
                    .Generate(10)
                .ToList();

            Clientes = clientes.Select(client => (ClientModel)client).ToList();

            var produtos = new Faker<Product>()
                    .CustomInstantiator(f => new Product(f.UniqueIndex, f.Commerce.ProductName(), 1000, decimal.Parse(f.Commerce.Price(1, 100, 2)), 500.00M))
                    .Generate(5)
                .ToList();

            Produtos = produtos.Select(product => (ProductModel) product).ToList();
        }
    }
}