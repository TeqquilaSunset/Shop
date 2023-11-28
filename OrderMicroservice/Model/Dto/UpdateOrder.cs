namespace OrderMicroservice.Model.Dto
{
    public class UpdateOrder
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
