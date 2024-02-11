using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Mappers;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderInterface;

        public OrderController(IOrderInterface orderInterface)
        {
            _orderInterface = orderInterface;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> orders = await _orderInterface.GetOrders();
            var ordersResponse = orders.Select(o => o.ToOrderResponseDTO());

            return Ok(ordersResponse);
            
        }
    }
}