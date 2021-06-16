using System;
using System.Threading.Tasks;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Events;
using DojoDDD.Infra.Providers.BusinessPeriod;
using DojoDDD.Infra.Providers.Schedulers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DojoDDD.Application.UseCases
{
    public class PurchaseOrderProcessingService : IPurchaseOrderProcessingService
    {
        private readonly IBusinessPeriodProvider _businessPeriodProvider;
        private readonly ICommandScheduleProvider<PurchaseOrderProcessingCommand> _scheduler;
        private readonly IWebHostEnvironment _environment;

        public PurchaseOrderProcessingService(IBusinessPeriodProvider businessPeriodProvider, ICommandScheduleProvider<PurchaseOrderProcessingCommand> scheduler, IWebHostEnvironment environment)
        {
            _businessPeriodProvider = businessPeriodProvider ?? throw new ArgumentNullException(nameof(businessPeriodProvider));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task Process(PurchaseOrderProcessingCommand command, Func<IEvent<PurchaseOrder>, Task> publish)
        {
            var period = await _businessPeriodProvider.GetBusinessPeriodAsync();

            if(period.IsOpen)
                return;

            var scheduleTo = _environment.IsDevelopment() ? DateTime.UtcNow.AddMinutes(1) : period.NextWindow.StartTime.AddMinutes(-15);
            await _scheduler.Process(new PurchaseOrderProcessingCommand(command.OrderId, scheduleTo));
        }
    }
}