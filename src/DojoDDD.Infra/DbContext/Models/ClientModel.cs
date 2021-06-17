using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.ValueObjects;

namespace DojoDDD.Infra.DbContext.Models
{
    public class ClientModel : IDataStoreModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public int Age { get; set; }
        public decimal Balance { get; set; }

        public static explicit operator Client(ClientModel model)
            => model is null ? null : new Client(model.Id, model.Name, model.Address, model.Age, model.Balance);

        public static implicit operator ClientModel(Client entity)
            => entity is null ? null : new ClientModel
            {
                    Id = entity.Id,
                    Name = entity.Name,
                    Address = entity.Address,
                    Age = entity.Age,
                    Balance = entity.Balance
            };
    }
}