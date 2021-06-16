using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Events;

namespace DojoDDD.Application.Abstractions.UseCases
{
    public interface IPurchaseOrderProcessingService
    {
        Task Process(PurchaseOrderProcessingCommand command, Func<IEvent<PurchaseOrder>, Task> publish);
    }
}