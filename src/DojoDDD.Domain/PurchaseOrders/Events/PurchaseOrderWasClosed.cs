using DojoDDD.Domain.PurchaseOrders.Entities;

namespace DojoDDD.Domain.PurchaseOrders.Events
{
    public class PurchaseOrderWasClosed : PurchaseOrderWasUpdated
    {
        public PurchaseOrderWasClosed(PurchaseOrder data) : base(data)
        {

        }

        public override string Name => nameof(PurchaseOrderWasClosed);
    }
}