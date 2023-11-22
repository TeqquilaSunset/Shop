using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.Helper;
using OrderMicroservice.Model.Dto;
using OrderMicroservice.Services;

namespace OrderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllOrders(Guid id)
        {
            var result = await _orderService.GetOrderAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder() 
        {
            var result = await _orderService.GetAllOrderAsync();
            return Ok(result);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var orderId = await _orderService.CreateOrderAsync(createOrderDto);

            // Если доставка курьером не требуется, то выполните какие-то другие действия
            if (!createOrderDto.DeliveryByCourier)
            {
                return Ok(orderId);
            }
            else
            {
                // Если требуется доставка курьером,  выполните соответствующие действия
                //var deliveryInfo = createOrderDto.DeliveryInfo;
                HttpRequestDelivery httpRequest = new();
                var r = await httpRequest.SendPostRequest(orderId);
                return Ok(r.Content);

            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            await _orderService.UpdateOrderAsync(createOrderDto);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _orderService.DeleteAsync(orderId);
            return Ok();
        }

        [HttpPut("cancel-order/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status502BadGateway)]
        //[Authorize(Roles = "Admin,Seller,Courier")]
        public async Task<IActionResult> CancelOrder([FromBody] Guid id)
        {
            await _orderService.CancellOrder(id);
            return Ok();
        }
    }
}
