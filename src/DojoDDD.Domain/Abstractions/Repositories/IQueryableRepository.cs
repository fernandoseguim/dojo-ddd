using System.Collections.Generic;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Specifications;

namespace DojoDDD.Domain.Abstractions.Repositories
{
    public interface IQueryableRepository<TQueryResult> where TQueryResult : class
    {
        Task<TQueryResult> GetAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<TQueryResult>;

        Task<ICollection<TQueryResult>> GetManyAsync<TSpec>(TSpec spec) where TSpec : QuerySpecification<TQueryResult>;

        Task<ICollection<TQueryResult>> GetAllAsync();
    }
}