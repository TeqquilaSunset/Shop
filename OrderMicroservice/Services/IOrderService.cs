using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;

namespace OrderMicroservice.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<ResponseOrders>> GetAllOrderAsync();
        public Task<ResponseOrders> GetOrderAsync(Guid id);
        public Task<Guid> CreateOrderAsync(CreateOrderDto orderDto);
        public Task DeleteAsync(Guid id);
        public Task UpdateOrderAsync(CreateOrderDto orderDto);
        public Task UpdateStatus(Guid id, OrderStatus status);
    }
}
