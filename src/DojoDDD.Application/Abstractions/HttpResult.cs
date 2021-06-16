using System.Collections.Generic;
using System.Text.Json.Serialization;
using DojoDDD.Domain.Errors;
using FluentResults;

namespace DojoDDD.Application.Abstractions
{
    public class HttpResult<T> : Result<T>
    {
        public HttpResult(int statusCode, T value)
        {
            StatusCode = statusCode;
            WithValue(value);
        }

        public HttpResult(int statusCode, IEnumerable<Error> errors)
        {
            StatusCode = statusCode;
            DetailedErrors = GetDetailedErrors(errors);

            WithErrors(DetailedErrors);
        }

        [JsonIgnore] public int StatusCode { get; }

        [JsonIgnore] public IEnumerable<DetailedError> DetailedErrors { get; }

        private static IEnumerable<DetailedError> GetDetailedErrors(IEnumerable<Error> errors)
        {
            var detailedErrors = new List<DetailedError>();

            foreach(var error in errors)
                if(error is DetailedError detailedError) detailedErrors.Add(detailedError);

            return detailedErrors;
        }
    }
}