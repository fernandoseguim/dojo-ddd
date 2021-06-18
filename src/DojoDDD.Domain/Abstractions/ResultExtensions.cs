using FluentResults;

namespace DojoDDD.Domain.Abstractions
{
    public static class ResultExtensions
    {
        public static Result Merge(this Result self, Result result)
            => self.WithReasons(result.Reasons);
    }
}