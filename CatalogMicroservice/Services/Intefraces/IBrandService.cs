using Shop.Model.Dtos;

namespace CatalogMicroservice.Services.Intefraces
{
    public interface IBrandService
    {
        public Task<IEnumerable<BrandDto>> GetAllAsync();
        public Task<BrandDto> GetBrandAsync(Guid id);
        public Task CreateBrandAsync(BrandDto brandDto);
        public Task UpdateBrandAsync(BrandDto brandDto);
        public Task DeleteBrandAsync(Guid id);
    }
}
