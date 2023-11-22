namespace OrderMicroservice.Model
{
    /// <summary>
    /// Модель заказа
    /// </summary>
    public class Order
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public bool DeliveryByCourier { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    /// <summary>
    /// Возможные статусы заказа
    /// </summary>
    public enum OrderStatus
    {
        Created,
        InDelivery,
        Completed,
        Canceled
    }
}
