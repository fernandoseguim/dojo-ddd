using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Commands;
using DojoDDD.Domain.Commands;

namespace DojoDDD.Infra.Providers.Schedulers
{
    public interface ICommandScheduleProvider<in TCommand> where TCommand : Command
    {
        Task Process(TCommand command);
    }
}