namespace DeliveryMicroservice.Model
{
    public class OrderStatusEnum
    {
        public enum OrderStatus
        {
            Created,
            InDelivery,
            Completed,
            Canceled
        }
    }
}
