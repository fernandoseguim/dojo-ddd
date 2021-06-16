using DojoDDD.Domain.Clients.Entities;
using NSpecifications;

namespace DojoDDD.Domain.Products.Specifications
{
    public class FindClientByIdSpec : Spec<Client>
    {
        public FindClientByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}