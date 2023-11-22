using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Model;
using Shop.Repositories;

namespace CatalogMicroservice.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly CatalogDbContext _dbContext;
        private bool disposedValue;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            // Проверка наличия категории и бренда в базе данных
            var existingCategory =  await _dbContext.Categories.FindAsync(product.CategoryId);
            var existingBrand = await _dbContext.Brands.FindAsync(product.BrandId);

            if (existingCategory == null || existingBrand == null)
            {
                throw new InvalidOperationException("Категория или бренд не найдены в базе данных.");
            }

            // Присоединение категории и бренда к продукту
            product.Category = existingCategory;
            product.Brand = existingBrand;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async void SaveAync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
