namespace OrderMicroservice.Model.Dto
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public bool DeliveryByCourier { get; set; }
        public DeliveryInfoDto DeliveryInfo { get; set; }
    }
}
