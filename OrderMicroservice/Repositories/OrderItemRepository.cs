using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;

namespace OrderMicroservice.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderItemRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(OrderItem entity)
        {
            await _dbContext.OrderItems.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            OrderItem? orderItem = await _dbContext.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _dbContext.OrderItems.Remove(orderItem);
                _dbContext.SaveChanges();
            }
        }

        public async Task AddRangeAsync(IEnumerable<OrderItem> orderItems)
        {
            //foreach (var item in orderItems)
            //{
            //    await _dbContext.OrderItems.AddAsync(item);
            //}

            await _dbContext.OrderItems.AddRangeAsync(orderItems);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(Guid orderId)
        {
            var ordersItem = await _dbContext.OrderItems.Where(i => i.OrderId == orderId).ToListAsync();
            return ordersItem;
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(id);
            return orderItem;
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            _dbContext.OrderItems.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var orderItems = await _dbContext.OrderItems.ToListAsync();
            return orderItems;
        }
    }
}
