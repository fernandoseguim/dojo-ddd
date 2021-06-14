using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Entities;

namespace DojoDDD.Domain.Abstractions.Repositories
{
    public interface IWritableRepository<in TEntity> where TEntity : Entity
    {
        Task SaveAsync(TEntity entity);
    }
}