using System;
using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.Clients.Entities
{
    public class Client : Entity, IEquatable<Client>
    {
        public Client(string id, string name, Address address, int age, decimal balance, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Address = address;
            Age = age;
            Balance = balance;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public string Id { get; }
        public string Name { get; }
        public Address Address { get; }
        public int Age { get; }
        public decimal Balance { get; private set; }

        public static Client Create(string name, Address address, int age, decimal balance)
            => new(Guid.NewGuid().ToString("N"), name, address, age, balance, DateTime.UtcNow, null);

        public void IncreaseBalance(decimal balance)
        {
            Balance += balance;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseBalance(decimal balance)
        {
            Balance -= balance;
            UpdatedAt = DateTime.UtcNow;
        }

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

        public override string GetAggregateId() => Id;
    }
}