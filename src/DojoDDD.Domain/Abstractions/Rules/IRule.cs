using System.Threading.Tasks;

namespace DojoDDD.Domain.Abstractions.Rules
{
    public interface IRule<in TEntity, TReason>
    {
        string Name { get; }

        Task<TReason> ApplyFrom(TEntity instance);
    }
}