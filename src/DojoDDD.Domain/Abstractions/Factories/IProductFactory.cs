using System.Threading.Tasks;

namespace DojoDDD.Domain.Abstractions.Factories
{
    public interface ISequenceNumberFactory
    {
        Task<int> Next(string key);
    }
}