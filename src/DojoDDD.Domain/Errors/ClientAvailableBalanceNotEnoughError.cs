namespace DojoDDD.Domain.Errors
{
    public class ClientAvailableBalanceNotEnoughError : DetailedError
    {
        private const string CODE = "SALDO_DO_CLIENTE_INSUFICIENTE";
        private const string MESSAGE = "Cliente não possui saldo suficiente para compra. Saldo disponível {0}";

        public ClientAvailableBalanceNotEnoughError(decimal balance) : base(CODE, string.Format(MESSAGE, balance))
        {
        }
    }
}