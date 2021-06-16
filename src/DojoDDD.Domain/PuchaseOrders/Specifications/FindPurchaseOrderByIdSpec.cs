using DojoDDD.Domain.PuchaseOrders.Entities;
using NSpecifications;

namespace DojoDDD.Domain.PuchaseOrders.Specifications
{
    public class FindPurchaseOrderByIdSpec : Spec<PurchaseOrder>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == id)
        {
        }
    }
}