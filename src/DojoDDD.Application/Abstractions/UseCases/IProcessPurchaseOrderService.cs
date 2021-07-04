using System;
using System.Threading.Tasks;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.PurchaseOrders.Events.StateTransfer;

namespace DojoDDD.Application.Abstractions.UseCases
{
    public interface IPurchaseOrderProcessingService
    {
        Task Process(PurchaseOrderProcessingCommand command, Func<IEvent<PurchaseOrder>, Task> publish);
    }
}