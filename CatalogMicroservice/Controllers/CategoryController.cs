using CatalogMicroservice.Services;
using CatalogMicroservice.Services.Intefraces;
using Microsoft.AspNetCore.Mvc;
using Shop.Model.Dtos;

namespace CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
