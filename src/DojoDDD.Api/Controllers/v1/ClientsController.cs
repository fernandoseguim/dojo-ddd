using System;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Api.Controllers.v1.Models;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v1
{
    [ApiController]
    [Route("clientes")]
    [ApiVersion("1.0")]
    [Obsolete("Prefer use v2")]
    public class ClientsController : Controller
    {
        private readonly IQueryableRepository<Client> _repository;

        public ClientsController(IQueryableRepository<Client> repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await _repository.GetAllAsync();
                if (clients == null)
                    return NoContent();

                return Ok(clients.Select(client => (ClientLegacy)client).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet("{idCliente}")]
        public async Task<IActionResult> GetById([FromRoute] string idCliente)
        {
            try
            {
                var client = await _repository.GetAsync(new FindClientByIdSpec(idCliente)).ConfigureAwait(false);
                if (client == null)
                    return NoContent();

                return Ok((ClientLegacy) client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}
