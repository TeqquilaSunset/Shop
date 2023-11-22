using AutoMapper;
using NuGet.Protocol.Core.Types;
using Shop.Model.Dtos;
using Shop.Model;
using Shop.Repositories;
using Microsoft.AspNetCore.Mvc;
using CatalogMicroservice.Services.Intefraces;

namespace Shop.Services
{
    public class BrandService : IBrandService
    {
        private IRepository<Brand> _repositoryBrand;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> repository, IMapper mapper)
        {
            _repositoryBrand = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _repositoryBrand.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetBrandAsync(Guid id)
        {
            var brand = await _repositoryBrand.GetByIdAsync(id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task CreateBrandAsync(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _repositoryBrand.AddAsync(brand);
        }

        public async Task UpdateBrandAsync(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _repositoryBrand.UpdateAsync(brand);
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            await _repositoryBrand.DeleteAsync(id);
        }
    }
}