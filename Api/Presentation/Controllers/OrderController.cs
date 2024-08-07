using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using api.Extenstions;
using Api.Core.Models;
using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Domain;
namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderInterface;

        private readonly IOrderItemInterface _orderItemInterface;
        
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderInterface orderInterface, UserManager<AppUser> userManager, IOrderItemInterface orderItemInterface)
        {
            _orderInterface = orderInterface;
            _userManager = userManager;

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

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder([FromQuery] int orderId)
        {
            string userName = User.GetUserName();
            AppUser ? user = await _userManager.FindByNameAsync(userName);

            var order = await _orderInterface.GetOrder(user.Id, orderId);
            
            return Ok(order.ToOrderResponseDTO());
            
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
            
            var order = await _orderInterface.GetOrder(null ,orderId);

            if(order == null)
                return NotFound("Order Not found");

            var orderitem = await _orderItemInterface.UpdateOrderItem(orderId, orderItemUpdateDTO.ProductId, orderItemUpdateDTO.Quantity);

            if(orderitem == null) return NotFound("Product Not found");

            return Ok(orderitem.ToOrderItemResponseDTO());
        }



    }
}