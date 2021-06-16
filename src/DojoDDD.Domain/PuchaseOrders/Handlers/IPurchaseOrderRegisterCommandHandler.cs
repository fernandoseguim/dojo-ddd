using System.Threading.Tasks;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using FluentResults;

namespace DojoDDD.Domain.PuchaseOrders.Handlers
{
    public interface IPurchaseOrderRegisterCommandHandler
    {
        Task<bool> ChangeStatusToAnalyzing(string orderId);

        Task<Result<PurchaseOrder>> HandleAsync(PurchaseOrderRegisterCommand command);
    }
}