using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogMicroservice.Services.Intefraces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Model;
using Shop.Model.Dtos;
using Shop.Repositories;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IRepository<Brand> db, IMapper mapper, IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Получение всех брендов
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _brandService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить бренд по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _brandService.GetBrandAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Создание нового брнеда
        /// </summary>
        /// <param name="brandDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BrandDto brandDto)
        {
            await _brandService.CreateBrandAsync(brandDto);
            return Ok();
        }

        /// <summary>
        /// Обновление бренда
        /// </summary>
        /// <param name="brandDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(BrandDto brandDto)
        {
            await _brandService.UpdateBrandAsync(brandDto);
            return Ok();
        }

        /// <summary>
        /// Удаление бренда
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _brandService.DeleteBrandAsync(id);
            return Ok();
        }

    }
}
