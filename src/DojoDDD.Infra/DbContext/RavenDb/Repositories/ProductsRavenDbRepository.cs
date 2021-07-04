using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DojoDDD.Infra.DbContext.RavenDb.Repositories
{
    public class ProductsRavenDbRepository : IEntityRepository<Product>, IQueryableRepository<ProductModel>, IDisposable
    {
        private readonly IAsyncDocumentSession _session;

        public ProductsRavenDbRepository(IDatabaseContext<IDocumentStore> context, IOptions<DbContextOptions> options)
        {
            if(options is null) throw new ArgumentNullException(nameof(options));

            var database = options.Value.Databases.FirstOrDefault();

            if(database is null) throw new InvalidOperationException();

            _session = context.Store.OpenAsyncSession(database);
        }

        public async Task<Product> GetAsync(string id)
        {
            var product = await _session.LoadAsync<ProductModel>(IdHelper.LoadForProducts(id));
            return (Product) product;
        }

        public async Task<ProductModel> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<ProductModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var products = await GetManyAsync(spec);
            return products.FirstOrDefault();
        }

        public async Task<ICollection<ProductModel>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<ProductModel>
        {
            if(spec is null) throw new ArgumentNullException(nameof(spec));

            var products = await _session.Query<ProductModel>().Where(spec.Expression).ToListAsync();

            return products;
        }

        public async Task<ICollection<ProductModel>> GetAllAsync()
        {
            var products = await _session.Query<ProductModel>().ToListAsync();

            return products;
        }

        public async Task SaveAsync(Product entity)
        {
            var model = (ProductModel)entity;

            await _session.StoreAsync(model);

            await _session.SaveChangesAsync();
        }

        #region DISPOSE

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _session?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ProductsRavenDbRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}