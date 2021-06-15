using DojoDDD.Domain.Entities;
using NSpecifications;

namespace DojoDDD.Domain.Specifications
{
    public class FindClientByIdSpec : Spec<Client>
    {
        public FindClientByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}