using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Domain.Clients.Commands
{
    public class CreateClientCommand
    {
        public CreateClientCommand(string name, Address address, int age, decimal balance)
        {
            Name = name;
            Address = address;
            Age = age;
            Balance = balance;
        }

        public string Name { get; }
        public Address Address { get; }
        public int Age { get; }
        public decimal Balance { get; }
    }
}