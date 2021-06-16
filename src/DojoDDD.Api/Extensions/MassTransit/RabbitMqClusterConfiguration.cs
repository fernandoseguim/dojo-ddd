namespace DojoDDD.Api.Extensions.MassTransit
{
    public class RabbitMqClusterConfiguration
    {
        public string Host { get; set; }
        public string[] Nodes { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}