using System.Threading.Tasks;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;

namespace DojoDDD.Application.Abstractions.UseCases
{
    public interface IPurchaseOrderRegisterService
    {
        Task<HttpResult<PurchaseOrder>> ProcessAsync(PurchaseOrderRegisterCommand command);
    }
}