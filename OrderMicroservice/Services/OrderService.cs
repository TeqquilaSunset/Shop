using AutoMapper;
using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;
using OrderMicroservice.Repositories;

namespace OrderMicroservice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository<Order> repository, IMapper mapper)
        {
            _orderRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseOrders>> GetAllOrderAsync()
        {
            var orders =  await _orderRepository.GetAllAsync();
            var result = (List<ResponseOrders>)_mapper.Map<IEnumerable<ResponseOrders>>(orders);
            return result;
        }

        public async Task<ResponseOrders> GetOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            var result = _mapper.Map<ResponseOrders>(order);
            return result;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto orderDto)
        {
            //_mapper.Map<Order>(orderDto);
            var orderItems = (List<OrderItem>)_mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems);
            var order = new Order()
            {
                CustomerName = orderDto.CustomerName,
                DeliveryByCourier = orderDto.DeliveryByCourier,
                OrderDate = DateTime.Now,
                OrderItems = orderItems,
                TotalAmount = CalculateTotalAmount(orderItems),
                Status = OrderStatus.Created
            };

            var id = await _orderRepository.AddAsync(order);
            return id;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }

        public async Task UpdateOrderAsync(CreateOrderDto orderDto)
        {
            var product = _mapper.Map<Order>(orderDto);
            await _orderRepository.UpdateAsync(product);
        }
        public async Task CancellOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            order.Status = OrderStatus.Canceled;
            await _orderRepository.UpdateAsync(order);
        }

        private decimal CalculateTotalAmount(List<OrderItem> orderItems)
        {
            return orderItems.Sum(item => item.Quantity * item.Price);
        }

        
    }
}
