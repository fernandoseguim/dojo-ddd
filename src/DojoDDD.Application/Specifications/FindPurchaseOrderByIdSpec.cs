using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Domain.PuchaseOrders.Entities;

namespace DojoDDD.Application.Specifications
{
    public class FindPurchaseOrderByIdSpec : QuerySpecification<PurchaseOrder>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}