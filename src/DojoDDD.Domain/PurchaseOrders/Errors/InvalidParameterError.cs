namespace DojoDDD.Domain.PurchaseOrders.Errors
{
    public class InvalidParameterError : DetailedError
    {
        public InvalidParameterError(string parameter, string message) : base("PARAMETRO_INVALIDO", message)
            => Parameter = parameter;

        public string Parameter { get; }
    }
}