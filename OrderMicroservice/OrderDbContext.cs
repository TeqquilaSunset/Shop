using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Model;
using System.Collections.Generic;

namespace OrderMicroservice
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryInfo> DeliveryInfo { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
