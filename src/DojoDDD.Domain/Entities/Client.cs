using System;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.Entities
{
    public class Client : IEquatable<Client>
    {
        public Client(string id, string name, Address address, int age, decimal balance)
        {
            Id = id;
            Name = name;
            Address = address;
            Age = age;
            Balance = balance;
        }

        public string Id { get; }
        public string Name { get; }
        public Address Address { get; }
        public int Age { get; }
        public decimal Balance { get; }

        public bool Equals(Client other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Client) obj);
        }

        public override int GetHashCode() => (Id != null ? Id.GetHashCode() : 0);

        public static bool operator ==(Client left, Client right) => Equals(left, right);

        public static bool operator !=(Client left, Client right) => !Equals(left, right);
    }
}