using Microsoft.EntityFrameworkCore;
using Shop.Model;
using System.Diagnostics.Eventing.Reader;

namespace Shop.Repositories
{
    public class BrandRepository : IRepository<Brand>, IDisposable
    {
        private readonly CatalogDbContext _dbContext;
        private bool disposedValue;

        public BrandRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Brand entity)
        {
            await _dbContext.Brands.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Brand? brand = await _dbContext.Brands.FindAsync(id);
            if (brand != null)
            {
                _dbContext.Brands.Remove(brand);
                _dbContext.SaveChanges();
            }
            //Тут нужна обработка ошибок
        }
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(Guid id)
        {
            return await _dbContext.Brands.FindAsync(id);
        }

        public async Task UpdateAsync(Brand entity)
        {
            _dbContext.Brands.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async void SaveAync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
