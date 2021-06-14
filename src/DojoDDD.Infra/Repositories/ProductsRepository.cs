using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Entities;
using DojoDDD.Infra.DbContext;
using NSpecifications;

namespace DojoDDD.Infra.Repositories
{
    public class ProductsRepository : IQueryableRepository<Product>
    {
        private readonly DataStore _dataStore;

        public ProductsRepository(DataStore dataStore) => _dataStore = dataStore;

        public async Task<Product> GetAsync<TSpec>(TSpec spec) where TSpec : ASpec<Product>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var products = await GetManyAsync(spec);
            return products.FirstOrDefault();
        }

        public async Task<ICollection<Product>> GetManyAsync<TSpec>(TSpec spec) where TSpec : ASpec<Product>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var expression = spec.Expression.Compile();
            var products = _dataStore.Produtos.Where(expression).ToList();
            return await Task.FromResult(products).ConfigureAwait(false);
        }

        public async Task<ICollection<Product>> GetAllAsync() => await Task.FromResult(_dataStore.Produtos).ConfigureAwait(false);
    }
}