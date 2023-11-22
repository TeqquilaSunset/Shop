using Shop.Model.Dtos;

namespace CatalogMicroservice.Services.Intefraces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDto>> GetAllAsync();
        public Task<CategoryDto> GetCategoryAsync(Guid id);
        public Task CreateCategoryAsync(CategoryDto categoryDto);
        public Task UpdateCategoryAsync(CategoryDto categoryDto);
        public Task DeleteCategoryAsync(Guid id);
    }
}
