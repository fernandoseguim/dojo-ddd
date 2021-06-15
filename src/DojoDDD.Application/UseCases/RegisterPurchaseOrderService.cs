using System;
using System.Threading.Tasks;
using DojoDDD.Application.Abstractions;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Domain.Abstractions.Handlers;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;
using FluentResults;

namespace DojoDDD.Application.UseCases
{
    public class PurchaseOrderRegisterService : IPurchaseOrderRegisterService
    {
        private readonly IPurchaseOrderRegisterCommandHandler _handler;

        public PurchaseOrderRegisterService(IPurchaseOrderRegisterCommandHandler handler)
            => _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        public async Task<HttpResult<PurchaseOrder>> ProcessAsync(PurchaseOrderRegisterCommand command)
        {
            if(command is null) throw new ArgumentNullException(nameof(command));

            if(command.HasError())
                return new HttpResult<PurchaseOrder>(400, command.Errors);

            var result = await _handler.HandleAsync(command);

            if(result.IsFailed)
                return new HttpResult<PurchaseOrder>(400, result.Errors);

            //check business hour

            //if in business hour
             //request risk analysis

            //else
             //schedule to next window

            return new HttpResult<PurchaseOrder>(200, result.Value);
        }
    }
}