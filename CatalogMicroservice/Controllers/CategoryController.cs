using CatalogMicroservice.Services;
using CatalogMicroservice.Services.Intefraces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить категорию по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Создание новой категории
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok();
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return Ok();
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manger, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
