using DojoDDD.Domain.Products.Entities;
using NSpecifications;

namespace DojoDDD.Domain.Clients.Specifications
{
    public class FindProductByIdSpec : Spec<Product>
    {
        public FindProductByIdSpec(int id) : base(data => data.Id == id)
        {
        }
    }
}