using System.Collections.Generic;
using System.Linq;
using Bogus;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Infra.DbContext
{
    public class DataStore
    {
        public List<PurchaseOrder> OrdensCompras { get; set; } = new List<PurchaseOrder>();

        public List<Client> Clientes { get; set; }
        public List<Product> Produtos { get; set; }

        public DataStore()
        {
            LoadFakeData();
        }

        private void LoadFakeData()
        {
            Clientes = new Faker<Client>()
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

            Produtos = new Faker<Product>()
                    .CustomInstantiator(f => new Product(f.UniqueIndex, f.Commerce.ProductName(), 1000, decimal.Parse(f.Commerce.Price(1, 100, 2)), 500.00M))
                    .Generate(5)
                .ToList();
        }
    }
}