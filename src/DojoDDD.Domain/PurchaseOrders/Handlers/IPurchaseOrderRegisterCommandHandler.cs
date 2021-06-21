using System.Threading.Tasks;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using FluentResults;

namespace DojoDDD.Domain.PurchaseOrders.Handlers
{
    public interface IPurchaseOrderRegisterCommandHandler
    {
        Task<bool> ChangeStatusToAnalyzing(string orderId);

        Task<Result<PurchaseOrder>> HandleAsync(PurchaseOrderRegisterCommand command);
    }
}