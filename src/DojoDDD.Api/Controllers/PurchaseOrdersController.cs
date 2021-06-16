using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DojoDDD.Application;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Specifications;
using MassTransit;

namespace DojoDDD.Api.Controllers
{
    [ApiController]
    [Route("ordem-compras")]
    public class PurchaseOrdersController : Controller
    {
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get([FromRoute] string orderId, [FromServices] IQueryableRepository<PurchaseOrder> repository)
        {
            var order = await repository.GetAsync(new FindPurchaseOrderByIdSpec(orderId));

            if(order is null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderRegisterCommand command, [FromServices] IPurchaseOrderRegisterService service, [FromServices] IBus bus)
        {
            var result = await service.ProcessAsync(command, @event => bus.Publish(@event));

            return result.IsFailed
                    ? StatusCode(result.StatusCode, result.DetailedErrors)
                    : StatusCode(result.StatusCode, result.Value);
        }
    }
}
