using AutoMapper;
using OrderMicroservice.Model;
using OrderMicroservice.Model.Dto;

namespace OrderMicroservice
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<ResponseOrders, Order>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();

            //CreateMap<IEnumerable<ResponseOrders>, IEnumerable<Order>>().ReverseMap();
            //CreateMap<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>().ReverseMap();
            //CreateMap<IEnumerable<OrderItemDto>, IEnumerable<OrderItem>>();

            //CreateMap<OrderItem, OrderItemDto>()
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            //    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            //CreateMap<OrderItemDto, OrderItem>()
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            //    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            // CreateMap<Order, CreateOrderDto>()
            //.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            //.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            //.ForMember(dest => dest.DeliveryByCourier, opt => opt.MapFrom(src => src.DeliveryByCourier));
            // CreateMap<CreateOrderDto, Order>()
            // .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            // .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            // .ForMember(dest => dest.DeliveryByCourier, opt => opt.MapFrom(src => src.DeliveryByCourier));

        }
    }
}
