using System;
using System.Threading.Tasks;
using DojoDDD.Api.Controllers.v1.Models;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Infra.DbContext.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v1
{
    [ApiController]
    [Route("ordemcompra")]
    [ApiVersion("1.0")]
    [Obsolete("Prefer use v2")]
    public class PurchaseOrderController : Controller
    {
        [HttpGet("{idOrdemCompra}")]
        public async Task<IActionResult> Get([FromRoute] string idOrdemCompra, [FromServices] IQueryableRepository<PurchaseOrderModel> repository)
        {
            try
            {
                var result = await repository.GetAsync(new FindPurchaseOrderByIdSpec(idOrdemCompra));
                return Ok((PurchaseOrderLegacy) result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.ToString() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderLegacyRequest request, [FromServices] IPurchaseOrderRegisterService service, [FromServices] IBus bus)
        {
            try
            {
                var result = await service.ProcessAsync(request, @event => bus.Publish(@event));

                //NOTE: This can broker contract with clients of v1 api.
                if(result.IsFailed) return BadRequest(result.DetailedErrors);

                return Created(string.Empty, result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.ToString() });
            }
        }
    }
}
