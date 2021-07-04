using System;
using System.Threading.Tasks;
using DojoDDD.Application.Abstractions;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.PurchaseOrders.Events.StateTransfer;
using DojoDDD.Domain.PurchaseOrders.Handlers;

namespace DojoDDD.Application.UseCases
{
    public class PurchaseOrderRegisterService : IPurchaseOrderRegisterService
    {
        private readonly IPurchaseOrderRegisterCommandHandler _handler;

        public PurchaseOrderRegisterService(IPurchaseOrderRegisterCommandHandler handler) => _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        public async Task<HttpResult<PurchaseOrder>> ProcessAsync(PurchaseOrderRegisterCommand command, Func<IEvent<PurchaseOrder>, Task> publish)
        {
            if(command is null) throw new ArgumentNullException(nameof(command));

            if(command.HasError())
                return new HttpResult<PurchaseOrder>(400, command.Errors);

            var result = await _handler.HandleAsync(command);

            if(result.IsFailed)
                return new HttpResult<PurchaseOrder>(400, result.Errors);

            await publish(result.Value.GetEvent<PurchaseOrderWasRequested, PurchaseOrder>());

            return new HttpResult<PurchaseOrder>(201, result.Value);
        }
    }
}