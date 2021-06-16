using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Specifications;

namespace DojoDDD.Api.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClientsController : Controller
    {
        private readonly IQueryableRepository<Client> _repository;

        public ClientsController(IQueryableRepository<Client> repository) => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _repository.GetAllAsync();

            if (clientes is null)
                return NoContent();

            return Ok(clientes);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetById([FromRoute] string clientId)
        {
            var clientes = await _repository.GetAsync(new FindClientByIdSpec(clientId)).ConfigureAwait(false);

            if (clientes is null)
                return NotFound();

            return Ok(clientes);
        }
    }
}
