using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Application.Specifications
{
    public class FindPurchaseOrderByIdSpec : QuerySpecification<PurchaseOrderQueryModel>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}