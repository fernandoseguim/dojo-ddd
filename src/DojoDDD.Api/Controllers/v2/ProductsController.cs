using System;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Factories;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Products.Commands;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DojoDDD.Api.Controllers.v2
{
    [ApiController]
    [Route("produtos")]
    [ApiVersion("2.0")]
    public class ProductsController : Controller
    {
        private readonly IQueryableRepository<ProductModel> _repository;

        public ProductsController(IQueryableRepository<ProductModel> repository) => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.GetAllAsync();

            if (products is null)
                return NoContent();

            return Ok(products.Select(model => (Product)model).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _repository.GetAsync(new FindProductByIdSpec(id.ToString())).ConfigureAwait(false);

            if (product is null)
                return NotFound();

            return Ok((Product) product);
        }

        public async Task<IActionResult> Post(CreateProductCommand command, [FromServices] IEntityRepository<Product> repository, [FromServices] ISequenceNumberFactory factory)
        {
            var id = await factory.Next("products");
            var product = Product.Create(id.ToString(), command.Description, command.AvailableQuantity, command.UnitPrice, command.PurchaseMinAmount);

            await repository.SaveAsync(product);

            return Created("", product);
        }

        [HttpGet("sequence")]
        public async Task<IActionResult> Get([FromServices] IDatabase repository)
        {
            var a = await repository.StringIncrementAsync("test");

            return Ok(a);
        }
    }
}
