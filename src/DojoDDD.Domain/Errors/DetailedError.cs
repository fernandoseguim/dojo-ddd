using System;
using FluentResults;

namespace DojoDDD.Domain.Errors
{
    public class DetailedError : Error, IEquatable<DetailedError>
    {
        public DetailedError(string code, string message)
        {
            Message = message;
            Code = code;
        }

        public string Code { get; }

        public bool Equals(DetailedError other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Code == other.Code && Message == other.Message;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DetailedError) obj);
        }

        public override int GetHashCode() => (Code != null ? Code.GetHashCode() : 0);

        public static bool operator ==(DetailedError left, DetailedError right) => Equals(left, right);

        public static bool operator !=(DetailedError left, DetailedError right) => !Equals(left, right);
    }
}