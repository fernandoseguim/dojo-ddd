using DojoDDD.Domain.Entities;
using NSpecifications;

namespace DojoDDD.Application.Specifications
{
    public class FindClientByIdSpec : Spec<Client>
    {
        public FindClientByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}