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
        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IApiService apiService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _apiService = apiService;
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

                //var deliveryInfo = createOrderDto.DeliveryInfo;
                var url = "https://localhost:7009/DeliveryMicroservice/api/Delivery";
                var result = await _apiService.PostAsync(deliveryDto, url);
                if(result.IsSuccessStatusCode)
                    return Ok(result);

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

        [HttpPut("update-order")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status502BadGateway)]
        //[Authorize(Roles = "Admin,Seller,Courier")]
        public async Task<IActionResult> CancelOrder([FromBody] UpdateOrder order)
        {
            await _orderService.UpdateStatus(order.Id, order.Status);
            return Ok();
        }
    }
}
