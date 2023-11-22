namespace OrderMicroservice.Model.Dto
{
    public class ResponseOrders
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public bool DeliveryByCourier { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
