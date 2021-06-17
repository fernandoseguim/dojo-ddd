﻿using System;
using System.Threading.Tasks;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Infra.DbContext.Models;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var product = await _repository.GetAsync(new FindProductByIdSpec(id.ToString())).ConfigureAwait(false);

            if (product is null)
                return NotFound();

            return Ok(product);
        }
    }
}