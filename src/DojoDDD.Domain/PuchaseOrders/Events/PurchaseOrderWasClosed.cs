using DojoDDD.Domain.PuchaseOrders.Entities;

namespace DojoDDD.Domain.PuchaseOrders.Events
{
    public class PurchaseOrderWasClosed : PurchaseOrderWasUpdated
    {
        public PurchaseOrderWasClosed(PurchaseOrder data) : base(data)
        {

        }

        public override string Name => nameof(PurchaseOrderWasClosed);
    }
}