using DeliveryMicroservice.Model;

namespace DeliveryMicroservice.Repositories
{
    public interface IDeliveryRepository
    {
        public Task<Guid> CreateDelivery(DeliveryOrder newDelivery);
        public Task<Guid> UpdateDelivery(DeliveryOrder newDelivery);
        public Task<Guid> DeleteDelivery(Guid deliveryId);
        public Task<DeliveryOrder> GetDeliveryOrder(Guid deliveryId);
        public Task<IEnumerable<DeliveryOrder>> GetAllDeliveryOrder();

    }
}
