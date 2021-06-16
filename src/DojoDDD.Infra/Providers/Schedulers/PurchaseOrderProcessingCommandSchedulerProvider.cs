using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Commands;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Specifications;
using MassTransit;

namespace DojoDDD.Infra.Providers.Schedulers
{
    public class MassTransitSchedulerProvider : ICommandScheduleProvider<PurchaseOrderProcessingCommand>
    {
        private readonly IEntityRepository<PurchaseOrder> _repository;
        private readonly IMessageScheduler _scheduler;

        public MassTransitSchedulerProvider(IEntityRepository<PurchaseOrder> repository, IBus bus)
        {
            if(bus is null) throw new ArgumentNullException(nameof(bus));

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _scheduler = bus.CreateMessageScheduler();
        }

        public async Task Process(PurchaseOrderProcessingCommand command)
        {
            var order = await _repository.GetAsync(new FindPurchaseOrderByIdSpec(command.OrderId));

            await order.Schedule(async () =>
            {
                var scheduledTo = command.ScheduleTo;
                await _scheduler.SchedulePublish(scheduledTo, command);

                return scheduledTo;
            });

            await _repository.SaveAsync(order);
        }
    }
}