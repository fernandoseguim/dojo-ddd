using System;
using System.Text;

namespace DojoDDD.Domain.ValueObjects
{
    public readonly struct Address : IEquatable<Address>
    {
        public Address(string zipCode,
            string addressLine,
            string buildingNumber,
            string neighborhood,
            string city,
            string state,
            string country,
            string complement
            )
        {
            ZipCode = zipCode;
            AddressLine = addressLine;
            BuildingNumber = buildingNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            Complement = complement;
        }

        public string ZipCode { get; }
        public string AddressLine { get; }
        public string BuildingNumber { get; }
        public string Neighborhood { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string Complement { get; }


        public bool Equals(Address other)
            => ZipCode == other.ZipCode
               && AddressLine == other.AddressLine
               && BuildingNumber == other.BuildingNumber
               && Neighborhood == other.Neighborhood
               && Country == other.Country
               && State == other.State
               && City == other.City
               && Complement == other.Complement;

        public override bool Equals(object obj)
            => obj is Address other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(ZipCode, AddressLine, BuildingNumber, Neighborhood, Country, State, City, Complement);

        public override string ToString()
        {
            const string separator = "-";
            var builder = new StringBuilder();

            builder.Append($"CEP. {ZipCode} {separator} ");
            builder.Append($"Rua/Av. {AddressLine} {separator} ");
            builder.Append($"Numero. {BuildingNumber} {separator} ");
            builder.Append($"Complemento. {Complement} {separator} ");
            builder.Append($"Bairro. {Neighborhood} {separator} ");
            builder.Append($"Cidade. {City} {separator} ");
            builder.Append($"Estado. {State} {separator} ");
            builder.Append($"Pais. {Country}");

            return builder.ToString();
        }
    }
}