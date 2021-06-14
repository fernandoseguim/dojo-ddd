using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DojoDDD.Application;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Aggregates;

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
        public async Task<IActionResult> Post([FromBody] PurchaseOrder ordemCompra, [FromServices] IPurchaseOrderService service)
        {
            var result = await service.Register(ordemCompra.ClientId, ordemCompra.ProductId, ordemCompra.OrderedQuantity);
            return Created(string.Empty, result);
        }
    }
}
