namespace OrderMicroservice.Model.Dto
{
    public class CreateDeliveryDto
    {
        public Guid OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }

    }
}
