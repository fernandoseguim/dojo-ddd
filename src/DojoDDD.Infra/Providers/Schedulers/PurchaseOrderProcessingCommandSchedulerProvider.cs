using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.ValueObjects;
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
            var order = await _repository.GetAsync(command.OrderId);

            await order.Schedule(async () =>
            {
                var scheduling = await _scheduler.SchedulePublish(command.Scheduling.Date, command);
                return new Scheduling(scheduling.TokenId, scheduling.ScheduledTime);
            });

            await _repository.SaveAsync(order);
        }
    }
}