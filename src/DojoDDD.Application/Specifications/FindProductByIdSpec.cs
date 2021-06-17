using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.Products.Entities;

namespace DojoDDD.Application.Specifications
{
    public class FindProductByIdSpec : QuerySpecification<Product>
    {
        public FindProductByIdSpec(int id) : base(data => data.Id == id)
        {
        }
    }
}