using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Application.Specifications
{
    public class FindProductByIdSpec : QuerySpecification<ProductModel>
    {
        public FindProductByIdSpec(string id) : base(data => data.Id == IdHelper.LoadForProducts(id))
        {
        }
    }
}