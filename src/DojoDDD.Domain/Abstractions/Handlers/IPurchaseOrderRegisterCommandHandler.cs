using System.Threading.Tasks;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;
using FluentResults;

namespace DojoDDD.Domain.Abstractions.Handlers
{
    public interface IPurchaseOrderRegisterCommandHandler
    {
        Task<bool> ChangeStatusToAnalyzing(string orderId);

        Task<Result<PurchaseOrder>> HandleAsync(PurchaseOrderRegisterCommand command);
    }
}