namespace DojoDDD.Domain.PuchaseOrders.Errors
{
    public class ProductQuantityNotEnoughError : DetailedError
    {
        private const string CODE = "QUANTIDADE_PRODUTO_INSUFICIENTE";
        private const string MESSAGE = "Quantidade em estoque não suficiente para compra. Quandidade disponível {0} e quandidade solicitada {1}";

        public ProductQuantityNotEnoughError(int availableQuantity, int requestedQuantity) : base(CODE, string.Format(MESSAGE, availableQuantity,requestedQuantity ))
        {
        }
    }
}