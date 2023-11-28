using DeliveryMicroservice.Model;
using DeliveryMicroservice.Repositories;

namespace DeliveryMicroservice.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Guid> CreateDeliveryAsync(DeliveryOrder order)
        {
            var ressult = await _deliveryRepository.CreateDelivery(order);
            return ressult;
        }

        public async Task DeleteDeliveryAsync(Guid id)
        {
            await _deliveryRepository.DeleteDelivery(id);
        }

        public async Task<IEnumerable<DeliveryOrder>> GetAllDeliveryAsync()
        {
            var ressult = await _deliveryRepository.GetAllDeliveryOrder();
            return ressult;
        }

        public async Task UpdateDeliveryAsync(DeliveryOrder deliveryOrder)
        {
            await _deliveryRepository.UpdateDelivery(deliveryOrder);
        }
    }
}
