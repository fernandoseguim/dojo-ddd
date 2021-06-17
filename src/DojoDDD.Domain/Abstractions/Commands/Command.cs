using System;
using System.Collections.Generic;
using System.Linq;
using DojoDDD.Domain.PuchaseOrders.Errors;
using FluentResults;
using FluentValidation;

namespace DojoDDD.Domain.Abstractions.Commands
{
    public class Command : IResultable
    {
        private readonly ICollection<Error> _errors;

        protected Command() => _errors = new HashSet<Error>();

        public IReadOnlyCollection<Error> Errors => _errors.ToList();

        public bool HasError() => _errors.Any();

        public bool HasError(Error error) => _errors.Any(error.Equals);

        public bool HasError<TError>() => _errors.OfType<TError>().Any();

        public bool NotHasError() => !HasError();

        protected void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            if(model is null) throw new ArgumentNullException(nameof(model));

            var result = validator.Validate(model);

            if (result.IsValid) return;

            foreach (var item in result.Errors)
                _errors.Add(new InvalidParameterError(item.PropertyName, item.ErrorMessage));
        }
    }
}