using System;
using System.Linq;
using System.Threading.Tasks;
using DojoDDD.Api.Controllers.v1.Models;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Specifications;
using DojoDDD.Domain.Products.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DojoDDD.Api.Controllers.v1
{
    [ApiController]
    [Route("produtos")]
    [ApiVersion("1.0")]
    [Obsolete("Prefer use v2")]
    public class ProdutoController : Controller
    {
        private readonly IQueryableRepository<Product> _repository;

        public ProdutoController(IQueryableRepository<Product> repository) => _repository = repository;

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                if (products == null)
                    return NoContent();

                return Ok(products.Select(product => (ProductLegacy) product).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var product = await _repository.GetAsync(new FindProductByIdSpec(id)).ConfigureAwait(false);
                if (product == null)
                    return NoContent();

                return Ok((ProductLegacy) product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}
