using DeliveryMicroservice.Model;

namespace DeliveryMicroservice.Services
{
    public interface IDeliveryService
    {
        public Task<Guid> CreateDeliveryAsync(DeliveryOrder order);
        public Task<IEnumerable<DeliveryOrder>> GetAllDeliveryAsync();
        public Task DeleteDeliveryAsync(Guid id);
        public Task UpdateDeliveryAsync(DeliveryOrder deliveryOrder);
       
    }
}
