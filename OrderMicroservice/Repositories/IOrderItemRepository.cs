using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;

namespace OrderMicroservice.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        public Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(Guid orderId);
        Task AddAsync(OrderItem entity);
        Task UpdateAsync(OrderItem entity);
        Task DeleteAsync(Guid id);
        Task AddRangeAsync(IEnumerable<OrderItem> orederItems);
    }
}
