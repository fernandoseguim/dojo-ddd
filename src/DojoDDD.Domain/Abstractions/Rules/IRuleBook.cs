using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;

namespace DojoDDD.Domain.Abstractions.Rules
{
    public interface IRuleBook<TEntity, TReason>
    {
        IEnumerable<IRule<TEntity, TReason>> Rules { get; }

        Task<Result<TEntity>> ApplyRules(TEntity entity);
    }
}