using AutoMapper;
using CatalogMicroservice.Services.Intefraces;
using Shop.Model;
using Shop.Model.Dtos;
using Shop.Repositories;

namespace CatalogMicroservice.Services
{
    public class ProductService : IProductService
    {
        private IRepository<Product> _repositoryProduct;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repositoryProduct = repository;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repositoryProduct.AddAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _repositoryProduct.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repositoryProduct.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var product = await _repositoryProduct.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repositoryProduct.UpdateAsync(product);
        }
    }
}
