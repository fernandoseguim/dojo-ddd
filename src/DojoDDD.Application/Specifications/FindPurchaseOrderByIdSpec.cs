using DojoDDD.Domain.Aggregates;
using NSpecifications;

namespace DojoDDD.Application.Specifications
{
    public class FindPurchaseOrderByIdSpec : Spec<PurchaseOrder>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}