using AutoMapper;
using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;
using OrderMicroservice.Repositories;

namespace OrderMicroservice.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        public OrderItemService(IOrderItemRepository orderItemService, IMapper mapper)
        {
            _orderItemRepository = orderItemService;
            _mapper = mapper;
        }

        public async Task CreateOrderItemsAsync(IEnumerable<OrderItemDto> orderDto, Guid oredrId)
        {
            var orderItems = _mapper.Map<IEnumerable<OrderItem>>(orderDto);
            List<OrderItem> list = new();

            foreach (var orderItemDto in orderItems)
            {
                //var item = _mapper.Map<OrderItem>(orderItemDto);
                orderItemDto.OrderId = oredrId;
                list.Add(orderItemDto);
            }
            await _orderItemRepository.AddRangeAsync(list);
        }

        public Task<IEnumerable<OrderItem>> GetAllOrderItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetOrderItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
