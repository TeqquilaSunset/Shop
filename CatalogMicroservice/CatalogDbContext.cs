using Microsoft.EntityFrameworkCore;
using Shop.Model;

namespace Shop
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
            Database.EnsureCreated();

        }

    }
}
