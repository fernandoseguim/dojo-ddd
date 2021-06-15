using DojoDDD.Domain.Entities;
using NSpecifications;

namespace DojoDDD.Domain.Specifications
{
    public class FindProductByIdSpec : Spec<Product>
    {
        public FindProductByIdSpec(int id) : base(data => data.Id == id)
        {
        }
    }
}