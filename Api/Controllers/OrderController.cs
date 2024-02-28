using Api.Dtos.OrderDTOS;
using Api.Interfaces;
using Api.Mappers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Api.Dtos.OrderItemDTO;
using api.Extenstions;
namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderInterface;

        private readonly IOrderItemInterface _orderItemInterface;
        private readonly IProductInterface _productInterface;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderInterface orderInterface, UserManager<AppUser> userManager, IProductInterface productInterface, IOrderItemInterface orderItemInterface)
        {
            _orderInterface = orderInterface;
            _userManager = userManager;
            _productInterface = productInterface;
            _orderItemInterface = orderItemInterface;
        }


        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            string userName = User.GetUserName();
            AppUser ? user = await _userManager.FindByNameAsync(userName);

            var orders = await _orderInterface.GetOrders(user.Id);
            var ordersResponse = orders.Select(o => o.ToOrderResponseDTO());

            return Ok(ordersResponse);
            
        }

        [HttpPost("CreateOrder")] 
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemAddDTO> orderItemAddDTOs)
        {   
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            var order = await _orderInterface.CreateOrder(user.Id);

            if(order == null)
                return BadRequest("Some Wrong happend");

           var orderItem = await _orderItemInterface.CreateOrderItem(order.OrderId, orderItemAddDTOs);

           if(orderItem == null)
                return NotFound("Some Product not found");

            return Ok(order.ToOrderResponseDTO());
 
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult>DeleteOrder([FromQuery] int orderId)
        {
            bool ? result = await _orderInterface.DeleteOrder(orderId);

            if(result == null) return NotFound("No order found with this Id");

            return Ok("Order has been deleted");
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult>UpdateOrder([FromQuery] int orderId, OrderItemUpdateDTO orderItemUpdateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var order = await _orderInterface.GetOrder(orderId);

            if(order == null)
                return NotFound("Order Not found");

            var orderitem = await _orderItemInterface.UpdateOrderItem(orderId, orderItemUpdateDTO.ProductId, orderItemUpdateDTO.Quantity);

            if(orderitem == null) return NotFound("Product Not found");

            return Ok(orderitem.ToOrderItemResponseDTO());
        }



    }
}