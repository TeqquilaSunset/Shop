using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Model;

namespace OrderMicroservice.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            var createOrder = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return createOrder.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            Order? order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var ordersWithItems = _dbContext.Orders.Include(o => o.OrderItems).ToList();
            return ordersWithItems;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var orderWithItems =  _dbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            return orderWithItems;
        }

        public async Task UpdateAsync(Order entity)
        {
            _dbContext.Orders.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
