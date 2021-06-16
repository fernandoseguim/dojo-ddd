using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Commands;

namespace DojoDDD.Infra.Providers.Schedulers
{
    public interface ICommandScheduleProvider<in TCommand> where TCommand : Command
    {
        Task Process(TCommand command);
    }
}