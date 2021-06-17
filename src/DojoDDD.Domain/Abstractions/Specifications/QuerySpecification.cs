using System;
using System.Linq.Expressions;
using NSpecifications;

namespace DojoDDD.Domain.Abstractions.Specifications
{
    public class QuerySpecification<TModel> : Spec<TModel>
    {
        public QuerySpecification(Expression<Func<TModel, bool>> expression) : base(expression)
        {
        }
    }
}