using OrderMicroservice.Model.Dto;
using OrderMicroservice.Model;

namespace OrderMicroservice.Services
{
    public interface IOrderItemService
    {
        public Task<IEnumerable<OrderItem>> GetAllOrderItemAsync();
        public Task<OrderItem> GetOrderItemAsync(Guid id);
        public Task CreateOrderItemsAsync(IEnumerable<OrderItemDto> orderDto, Guid orderId);
    }
}
