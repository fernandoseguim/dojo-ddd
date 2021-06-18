using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Factories;
using StackExchange.Redis;

namespace DojoDDD.Infra.Factories
{
    public class RedisSequenceNumberFactory : ISequenceNumberFactory
    {
        private readonly IDatabase _database;

        public RedisSequenceNumberFactory(IDatabase database) => _database = database;

        public async Task<int> Next(string key)
        {
            var number = await _database.StringIncrementAsync($"sequences:{key}".ToLower());
            return (int)number;
        }
    }
}