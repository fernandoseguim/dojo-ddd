using DojoDDD.Domain.Clients.Entities;

namespace DojoDDD.Api.Controllers.v1.Models
{
    public class ClientLegacy
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public int Idade { get; set; }
        public decimal Saldo { get; set; }

        public static implicit operator ClientLegacy(Client entity)
            => entity is null ? null : new ClientLegacy
            {
                    Id = entity.Id,
                    Nome = entity.Name,
                    Endereco = entity.Address.ToString(),
                    Idade = entity.Age,
                    Saldo = entity.Balance
            };
    }
}