namespace DojoDDD.Domain.PuchaseOrders.Errors
{
    public class InvalidParameterError : DetailedError
    {
        public InvalidParameterError(string parameter, string message) : base("PARAMETRO_INVALIDO", message)
            => Parameter = parameter;

        public string Parameter { get; }
    }
}