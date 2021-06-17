using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Products.Entities;

namespace DojoDDD.Infra.DbContext.InMemory.Repositories
{
    public class ProductsInMemoryRepository : IQueryableRepository<Product>
    {
        private readonly DataStore _dataStore;

        public ProductsInMemoryRepository(DataStore dataStore) => _dataStore = dataStore;

        public Task<Product> GetAsync(string id)
        {
            var product = _dataStore.Produtos.FirstOrDefault(c => id.Equals(c.Id.ToString()));

            return Task.FromResult(product);
        }

        public async Task<Product> GetAsync<TSpec>(TSpec spec) where TSpec : Specification<Product>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var products = await GetManyAsync(spec);
            return products.FirstOrDefault();
        }

        public async Task<ICollection<Product>> GetManyAsync<TSpec>(TSpec spec) where TSpec : Specification<Product>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var expression = spec.Expression.Compile();
            var products = _dataStore.Produtos.Where(expression).ToList();
            return await Task.FromResult(products).ConfigureAwait(false);
        }

        public async Task<ICollection<Product>> GetAllAsync() => await Task.FromResult(_dataStore.Produtos).ConfigureAwait(false);
    }
}
