using DeliveryMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace DeliveryMicroservice
{
    public class DeliveryDbContext : DbContext
    {
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }

        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
