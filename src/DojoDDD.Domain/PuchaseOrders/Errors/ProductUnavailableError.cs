namespace DojoDDD.Domain.PuchaseOrders.Errors
{
    public class ProductUnavailableError : DetailedError
    {
        private const string CODE = "PRODUTO_NAO_DISPONIVEL";
        private const string MESSAGE = "Produto indisponível para a compra";

        public ProductUnavailableError() : base(CODE, MESSAGE)
        {
        }
    }
}