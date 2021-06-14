using System.Collections.Generic;
using FluentResults;

namespace DojoDDD.Domain.Abstractions.Entities
{
    public interface IResultable
    {
        public IReadOnlyCollection<Error> Errors { get; }

        public bool HasError();
        public bool HasError(Error error);
        public bool HasError<TError>();

        public bool NotHasError();
    }
}