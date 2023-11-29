using DeliveryMicroservice.Model;
using DeliveryMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private IDeliveryService _deliveryService;
        private IApiService _apiService;
        IHttpContextAccessor _httpContextAccessor;
        public DeliveryController(IDeliveryService deliveryService, IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _deliveryService = deliveryService;
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получить все доставки
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "DeliveryMan, Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _deliveryService.GetAllDeliveryAsync();
            return Ok(result);
        }

        /// <summary>
        /// Создание доставки
        /// </summary>
        /// <param name="newDelivery"></param>
        /// <returns></returns>
        [Authorize(Roles = "User, Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryOrder newDelivery)
        {
            var result = await _deliveryService.CreateDeliveryAsync(newDelivery);
            return Ok(result);
        }

        /// <summary>
        /// Обновление существующей доставки
        /// </summary>
        /// <param name="newDelivery"></param>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateDelivery(DeliveryOrder newDelivery)
        {
            await _deliveryService.UpdateDeliveryAsync(newDelivery);
            return Ok();
        }

        /// <summary>
        /// Сбор заказа, курером и обновление статуса на «InDelivery»
        /// </summary>
        /// <param name="orderId"></param>
        [HttpPut("collect-order")]
        public async Task<IActionResult> CollectAnOrder(Guid orderId)
        {
            string url = "https://localhost:7009/OrderMicroservice/api/Order/update-order/";
            var order = new
            {
                Id = orderId,
                Status = OrderStatusEnum.OrderStatus.InDelivery,
            };

            string headerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string cookieToken = _httpContextAccessor.HttpContext.Request.Cookies["auth_access_token"];
            HttpResponseMessage result;

            if (cookieToken != null)
            {
                result = await _apiService.PutAsync(order, url, cookieToken);
            }
            else
            {
                result = await _apiService.PutAsync(order, url, headerToken);
            }
            
            return Ok(result);
        }
    }
}
