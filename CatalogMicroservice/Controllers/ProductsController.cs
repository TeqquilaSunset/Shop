using AutoMapper;
using CatalogMicroservice.Services.Intefraces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Model;
using Shop.Model.Dtos;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productService.GetProductAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.CreateProductAsync(productDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _productService.UpdateProductAsync(productDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
