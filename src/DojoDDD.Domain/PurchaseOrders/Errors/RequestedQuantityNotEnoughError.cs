using System;

namespace DojoDDD.Domain.PurchaseOrders.Errors
{
    public class RequestedQuantityNotEnoughError : DetailedError
    {
        private const string CODE = "QUANTIDADE_SOLICITADA_NAO_SUFICIENTE";
        private const string MESSAGE = "Quantidade solicitada não suficiente para compra. Quantidade solicitada {0}";

        public RequestedQuantityNotEnoughError(int requestedQuantity) : base(CODE, String.Format(MESSAGE, requestedQuantity))
        {
        }
    }
}