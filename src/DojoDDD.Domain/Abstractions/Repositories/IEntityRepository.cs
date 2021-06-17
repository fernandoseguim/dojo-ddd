using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;

namespace DojoDDD.Domain.Abstractions.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetAsync(string id);
        Task SaveAsync(TEntity entity);
    }
}