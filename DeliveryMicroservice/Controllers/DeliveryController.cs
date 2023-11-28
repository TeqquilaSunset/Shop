using DeliveryMicroservice.Model;
using DeliveryMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private IDeliveryService _deliveryService;
        private IApiService _apiService;
        public DeliveryController(IDeliveryService deliveryService, IApiService apiService)
        {
            _deliveryService = deliveryService;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _deliveryService.GetAllDeliveryAsync();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryOrder newDelivery)
        {
            var result = await _deliveryService.CreateDeliveryAsync(newDelivery);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDelivery(DeliveryOrder newDelivery)
        {
            await _deliveryService.UpdateDeliveryAsync(newDelivery);
            return Ok();
        }

        [HttpPut("collect-order")]
        public async Task<IActionResult> CollectAnOrder(Guid orderId)
        {
            string url = "https://localhost:7009/OrderMicroservice/api/Order/update-order/";
            var order = new
            {
                Id = orderId,
                Status = OrderStatusEnum.OrderStatus.InDelivery,
            };

            var result = await _apiService.PutAsync(order, url);
            return Ok(result);
        }
    }
}
