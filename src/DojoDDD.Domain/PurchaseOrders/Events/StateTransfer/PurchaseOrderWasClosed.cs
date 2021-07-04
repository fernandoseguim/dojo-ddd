using DojoDDD.Domain.PurchaseOrders.Entities;

namespace DojoDDD.Domain.PurchaseOrders.Events.StateTransfer
{
    public class PurchaseOrderWasClosed : PurchaseOrderWasUpdated
    {
        public PurchaseOrderWasClosed(PurchaseOrder data) : base(data)
        {

        }

        public override string Name => nameof(PurchaseOrderWasClosed);
    }
}