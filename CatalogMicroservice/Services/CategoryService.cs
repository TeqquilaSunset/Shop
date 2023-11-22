using AutoMapper;
using CatalogMicroservice.Services.Intefraces;
using Shop.Model;
using Shop.Model.Dtos;
using Shop.Repositories;
using System.Drawing.Drawing2D;

namespace CatalogMicroservice.Services
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _repositoryCategory;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repositoryCategory = repository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repositoryCategory.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _repositoryCategory.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repositoryCategory.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid id)
        {
            var category = await _repositoryCategory.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repositoryCategory.UpdateAsync(category);
        }

    }
}
