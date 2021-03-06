using System;
using System.Threading.Tasks;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Events;

namespace DojoDDD.Application.Abstractions.UseCases
{
    public interface IPurchaseOrderRegisterService
    {
        Task<HttpResult<PurchaseOrder>> ProcessAsync(PurchaseOrderRegisterCommand command, Func<IEvent<PurchaseOrder>, Task> publish);
    }
}