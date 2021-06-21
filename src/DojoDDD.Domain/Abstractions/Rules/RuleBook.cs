using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DojoDDD.Domain.PurchaseOrders.Errors;
using FluentResults;

namespace DojoDDD.Domain.Abstractions.Rules
{
    public abstract class RuleBook<TEntity> : IRuleBook<TEntity, DetailedError>
    {
        public IEnumerable<IRule<TEntity, DetailedError>> Rules { get; }

        protected RuleBook(IEnumerable<IRule<TEntity, DetailedError>> rules) => Rules = rules ?? throw new ArgumentNullException(nameof(rules));

        public virtual async Task<Result<TEntity>> ApplyRules(TEntity entity)
        {
            var result = new Result<TEntity>().WithValue(entity);

            foreach(var rule in Rules)
            {
                var reason = await rule.ApplyFrom(entity);

                if(reason is not null)
                    result.WithReason(reason);
            }

            return result;
        }
    }
}