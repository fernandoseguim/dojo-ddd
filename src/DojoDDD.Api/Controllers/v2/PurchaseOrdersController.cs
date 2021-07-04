using System.Threading.Tasks;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.PurchaseOrders.Commands;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Events;
using DojoDDD.Domain.PurchaseOrders.Events.StateTransfer;
using DojoDDD.Infra.DbContext.Models;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v2
{
    [ApiController]
    [Route("ordens-compra")]
    [ApiVersion("2.0")]
    public class PurchaseOrdersController : Controller
    {
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get([FromRoute] string orderId, [FromServices] IQueryableRepository<PurchaseOrderModel> repository)
        {
            var order = await repository.GetAsync(new FindPurchaseOrderByIdSpec(orderId));

            if(order is null) return NotFound();

            return Ok((PurchaseOrder) order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderRegisterCommand command, [FromServices] IPurchaseOrderRegisterService service, [FromServices] ITopicProducer<IEvent<PurchaseOrder>> bus)
        {
            var result = await service.ProcessAsync(command, @event => bus.Produce(@event));

            return result.IsFailed
                    ? StatusCode(result.StatusCode, result.DetailedErrors)
                    : StatusCode(result.StatusCode, result.Value);
        }
    }
}
