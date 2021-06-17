using System.Collections.Generic;

namespace DojoDDD.Api.Extensions.MassTransit
{
    public class RedisClusterConnection
    {
        public string Host { get; set; }
        public Dictionary<string, string> Prefixes { get; set; }
    }
}