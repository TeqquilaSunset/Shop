using Shop.Model.Dtos;

namespace CatalogMicroservice.Services.Intefraces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllAsync();
        public Task<ProductDto> GetProductAsync(Guid id);
        public Task CreateProductAsync(ProductDto productDto);
        public Task UpdateProductAsync(ProductDto productDto);
        public Task DeleteProductAsync(Guid id);
    }
}
