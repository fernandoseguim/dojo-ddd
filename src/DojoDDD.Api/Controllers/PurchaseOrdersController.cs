using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DojoDDD.Application;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Handlers;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Commands;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Handlers;

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
        public async Task<IActionResult> Post([FromBody] PurchaseOrderRegisterCommand command, [FromServices] IPurchaseOrderRegisterService service)
        {
            var result = await service.ProcessAsync(command);

            return result.IsFailed
                    ? StatusCode(result.StatusCode, result.DetailedErrors)
                    : StatusCode(result.StatusCode, result.Value);
        }
    }
}
