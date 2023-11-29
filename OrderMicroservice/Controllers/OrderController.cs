using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.Model.Dto;
using OrderMicroservice.Services;
using static System.Net.WebRequestMethods;

namespace OrderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IApiService apiService
            , IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получение заказа по id
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _orderService.GetOrderAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка всех заказов
        /// </summary>
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrderAsync();
            return Ok(result);
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="createOrderDto"></param>
        [Authorize(Roles = "User, Manager, Admin")]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var orderId = await _orderService.CreateOrderAsync(createOrderDto);

            if (!createOrderDto.DeliveryByCourier)
            {
                return Ok(orderId);
            }
            else
            {
                CreateDeliveryDto deliveryDto = new CreateDeliveryDto();
                deliveryDto.OrderId = orderId;
                deliveryDto.DeliveryDate = createOrderDto.DeliveryInfo.DeliveryDate;
                deliveryDto.DeliveryAddress = createOrderDto.DeliveryInfo.DeliveryAddress;

                var url = "https://localhost:7009/DeliveryMicroservice/api/Delivery";
                string cookieToken = _httpContextAccessor.HttpContext.Request.Cookies["auth_access_token"];
                var result = await _apiService.PostAsync(deliveryDto, url, cookieToken);
                if (!result.IsSuccessStatusCode)
                    return BadRequest("Не удалось создать запрос на доставку");
                return Ok(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// Обновление существующего заказа
        /// </summary>
        /// <param name="createOrderDto"></param>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            await _orderService.UpdateOrderAsync(createOrderDto);
            return Ok();
        }

        /// <summary>
        /// Удаление заказа по id
        /// </summary>
        /// <param name="orderId"></param>
        [Authorize(Roles = "Manager, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _orderService.DeleteAsync(orderId);
            return Ok();
        }


        /// <summary>
        /// Обновление статуса заказа
        /// </summary>
        /// <param name="order"></param>
        [Authorize(Roles = "DeliveryMan, Manager, Admin")]
        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateStatusOrder([FromBody] UpdateOrder order)
        {
            await _orderService.UpdateStatus(order.Id, order.Status);
            return Ok();
        }
    }
}
