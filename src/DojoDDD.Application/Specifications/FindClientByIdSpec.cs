using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Clients.Entities;

namespace DojoDDD.Application.Specifications
{
    public class FindClientByIdSpec : QuerySpecification<Client>
    {
        public FindClientByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}