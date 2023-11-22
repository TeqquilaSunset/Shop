using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogMicroservice.Services.Intefraces;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _brandService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _brandService.GetBrandAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandDto brandDto)
        {
            await _brandService.CreateBrandAsync(brandDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BrandDto brandDto)
        {
            await _brandService.UpdateBrandAsync(brandDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _brandService.DeleteBrandAsync(id);
            return Ok();
        }

    }
}
