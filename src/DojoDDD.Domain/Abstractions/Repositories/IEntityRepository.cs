using DojoDDD.Domain.Abstractions.Entities;

namespace DojoDDD.Domain.Abstractions.Repositories
{
    public interface IEntityRepository<TEntity> : IQueryableRepository<TEntity>, IWritableRepository<TEntity> where TEntity : Entity { }
}