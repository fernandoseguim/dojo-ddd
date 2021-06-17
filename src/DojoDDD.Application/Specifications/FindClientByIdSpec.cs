using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Application.Specifications
{
    public class FindClientByIdSpec : QuerySpecification<ClientModel>
    {
        public FindClientByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}