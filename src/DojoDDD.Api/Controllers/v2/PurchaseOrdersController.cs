using System.Threading.Tasks;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Infra.DbContext.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v2
{
    [ApiController]
    [Route("ordens-compra")]
    [ApiVersion("2.0")]
    public class PurchaseOrdersController : Controller
    {
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get([FromRoute] string orderId, [FromServices] IQueryableRepository<PurchaseOrderQueryModel> repository)
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
