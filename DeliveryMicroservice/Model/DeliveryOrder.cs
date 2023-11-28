using System.Diagnostics.Metrics;

namespace DeliveryMicroservice.Model
{
    public class DeliveryOrder
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }

        public bool Finished { get; set; } = false;
    }

}
