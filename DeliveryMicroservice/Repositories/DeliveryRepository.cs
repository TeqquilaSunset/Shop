using DeliveryMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace DeliveryMicroservice.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryDbContext _dbContext;
        public DeliveryRepository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateDelivery(DeliveryOrder newDelivery)
        {
            var result = await _dbContext.DeliveryOrders.AddAsync(newDelivery);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<Guid> DeleteDelivery(Guid deliveryId)
        {
            var delivery = await _dbContext.DeliveryOrders.FindAsync(deliveryId);
            if (delivery != null)
            {
                var result = _dbContext.DeliveryOrders.Remove(delivery);
                await _dbContext.SaveChangesAsync();
                return result.Entity.Id;
            }

            return Guid.Empty;
        }

        public async Task<IEnumerable<DeliveryOrder>> GetAllDeliveryOrder()
        {
            var result = await _dbContext.DeliveryOrders.ToListAsync();
            return result;
        }

        public async Task<DeliveryOrder> GetDeliveryOrder(Guid deliveryId)
        {
            var delivery = await _dbContext.DeliveryOrders.FindAsync(deliveryId);

            return delivery;

        }

        public async Task<Guid> UpdateDelivery(DeliveryOrder delivery)
        {
            _dbContext.DeliveryOrders.Update(delivery);
            await _dbContext.SaveChangesAsync();
            return delivery.Id;
        }
    }
}
