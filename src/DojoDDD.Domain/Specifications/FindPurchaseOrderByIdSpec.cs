using DojoDDD.Domain.Entities;
using NSpecifications;

namespace DojoDDD.Domain.Specifications
{
    public class FindPurchaseOrderByIdSpec : Spec<PurchaseOrder>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}