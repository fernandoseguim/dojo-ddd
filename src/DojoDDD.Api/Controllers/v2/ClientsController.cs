using System;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Commands;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v2
{
    [ApiController]
    [Route("clientes")]
    [ApiVersion("2.0")]
    public class ClientsController : Controller
    {
        private readonly IQueryableRepository<ClientModel> _repository;

        public ClientsController(IQueryableRepository<ClientModel> repository) => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _repository.GetAllAsync();

            if (clientes is null)
                return NoContent();

            return Ok(clientes.Select(model => (Client)model).ToList());
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetById([FromRoute] string clientId)
        {
            var client = await _repository.GetAsync(new FindClientByIdSpec(clientId)).ConfigureAwait(false);

            if (client is null)
                return NotFound();

            return Ok((Client) client);
        }

        public async Task<IActionResult> Post(CreateClientCommand command, [FromServices] IEntityRepository<Client> repository)
        {
            var client = Client.Create(command.Name, command.Address, command.Age, command.Balance);

            await repository.SaveAsync(client);

            return Created("", client);
        }
    }
}
