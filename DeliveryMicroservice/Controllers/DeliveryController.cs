using DeliveryMicroservice.Model;
using DeliveryMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
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
    }
}
