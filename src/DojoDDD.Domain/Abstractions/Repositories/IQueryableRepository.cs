using System.Collections.Generic;
using System.Threading.Tasks;
using NSpecifications;

namespace DojoDDD.Domain.Abstractions.Repositories
{
    public interface IQueryableRepository<TQueryResult> where TQueryResult : class
    {
        Task<TQueryResult> GetAsync<TSpec>(TSpec spec) where TSpec : ASpec<TQueryResult>;

        Task<ICollection<TQueryResult>> GetManyAsync<TSpec>(TSpec spec) where TSpec : ASpec<TQueryResult>;

        Task<ICollection<TQueryResult>> GetAllAsync();
    }
}