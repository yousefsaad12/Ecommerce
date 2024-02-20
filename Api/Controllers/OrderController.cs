using Api.Dtos.OrderDTOS;
using Api.Interfaces;
using Api.Mappers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderInterface;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderInterface orderInterface, UserManager<AppUser> userManager)
        {
            _orderInterface = orderInterface;
             _userManager = userManager;
        }


        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> orders = await _orderInterface.GetOrders();
            var ordersResponse = orders.Select(o => o.ToOrderResponseDTO());

            return Ok(ordersResponse);
            
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult>CreateOrder(OrderCreateDTO orderCreate)
        {   
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Ok("k");
        }
    }
}