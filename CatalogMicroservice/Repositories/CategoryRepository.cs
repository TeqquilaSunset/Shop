using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Model;
using Shop.Repositories;

namespace CatalogMicroservice.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly CatalogDbContext _dbContext;

        public CategoryRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Category? category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async void SaveAync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
