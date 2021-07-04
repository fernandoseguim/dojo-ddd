using System;
using System.Threading.Tasks;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.PurchaseOrders.Events.StateTransfer;
using MassTransit;

namespace DojoDDD.Api.Consumers
{
    public class PurchaseOrderWasRequestedConsumer : IConsumer<IEvent<PurchaseOrder>>
    {
        private readonly IPurchaseOrderProcessingService _service;

        public PurchaseOrderWasRequestedConsumer(IPurchaseOrderProcessingService service)
            => _service = service ?? throw new ArgumentNullException(nameof(service));

        public Task Consume(ConsumeContext<IEvent<PurchaseOrder>> context)
        {
            var message = context.Message;

            return _service.Process(new PurchaseOrderProcessingCommand(message.EntityId), @event => context.Publish(@event));
        }
    }

    public class PurchaseOrderProcessingCommandConsumer : IConsumer<PurchaseOrderProcessingCommand>
    {
        private readonly IPurchaseOrderProcessingService _service;

        public PurchaseOrderProcessingCommandConsumer(IPurchaseOrderProcessingService service)
            => _service = service ?? throw new ArgumentNullException(nameof(service));

        public Task Consume(ConsumeContext<PurchaseOrderProcessingCommand> context)
        {
            var message = context.Message;

            return _service.Process(message, @event => context.Publish(@event));
        }
    }
}