using System.Collections.Generic;
using System.Linq;
using FluentResults;

namespace DojoDDD.Domain.Abstractions.Entities
{
    public abstract class Entity : IResultable
    {
        private readonly ICollection<Error> _errors;
        protected Entity() => _errors = new List<Error>();

        public IReadOnlyCollection<Error> Errors => _errors.ToList();
        public bool NotHasError() => !HasError();
        public bool HasError() => _errors.Any();
        public bool HasError<TError>() => _errors.OfType<TError>().Any();

        public bool HasError(Error error) => _errors.Any(error.Equals);

        protected void AppendErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                AppendError(error);
            }
        }

        protected void AppendError(Error error) => _errors.Add(error);
    }
}
