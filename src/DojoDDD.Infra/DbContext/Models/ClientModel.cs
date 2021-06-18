using System;
using DojoDDD.Domain.Abstractions.Entities;
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
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public static explicit operator Client(ClientModel model)
            => model is null ? null : new Client(IdHelper.RemoveClientsPrefix(model.Id), model.Name, model.Address, model.Age, model.Balance, model.CreatedAt, model.UpdatedAt);

        public static implicit operator ClientModel(Client entity)
            => entity is null ? null : new ClientModel
            {
                    Id = IdHelper.LoadForClients(entity.Id),
                    Name = entity.Name,
                    Address = entity.Address,
                    Age = entity.Age,
                    Balance = entity.Balance,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
            };
    }
}