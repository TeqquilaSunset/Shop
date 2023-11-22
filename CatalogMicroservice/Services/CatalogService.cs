using CatalogMicroservice.Model.Dtos;
using Shop.Model.Dtos;
using Shop.Model;
using Shop.Repositories;
using CatalogMicroservice.Services.Intefraces;
using CatalogMicroservice.Repositories;
using AutoMapper;

namespace CatalogMicroservice.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public CatalogService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IRepository<Brand> brandRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CatalogDto> GetCatalogAsync()
        {
            var catalogDto = new CatalogDto();

            var products = await _productRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            catalogDto.Products = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Brand = _mapper.Map<BrandDto>(brands.FirstOrDefault(b => b.Id == p.BrandId)),
                Category = _mapper.Map<CategoryDto>(categories.FirstOrDefault(c => c.Id == p.CategoryId)) 
            }).ToList();

            return catalogDto;
        }
    }
}
