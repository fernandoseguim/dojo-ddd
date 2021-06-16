using System;
using DojoDDD.Domain.Entities;

namespace DojoDDD.Domain.Events
{
    public class PurchaseOrderWasClosed : PurchaseOrderWasUpdated
    {
        public PurchaseOrderWasClosed(PurchaseOrder data) : base(data)
        {

        }

        public override string Name => nameof(PurchaseOrderWasClosed);
    }
}