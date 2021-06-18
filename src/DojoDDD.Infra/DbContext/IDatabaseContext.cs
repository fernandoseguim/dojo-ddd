using System.Threading.Tasks;

namespace DojoDDD.Infra.DbContext
{
    public interface IDatabaseContext<out TContext>
    {
        TContext Store { get; }

        Task ConfigureAsync();

        Task<bool> Exist(string database);
    }
}
