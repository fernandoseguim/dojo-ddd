using System;

namespace DojoDDD.Domain.Errors
{
    public class PurchaseMinAmountNotReachedError : DetailedError
    {
        private const string CODE = "VALOR_MINIMO_NAO_ATINGIDO";
        private const string MESSAGE = "Valor mínimo para a compra não atingido. Valor minimo {0} e valor da order de compra {1}";

        public PurchaseMinAmountNotReachedError(decimal minAmount, decimal orderAmount) : base(CODE, string.Format(MESSAGE, minAmount, orderAmount))
        {
        }
    }
}