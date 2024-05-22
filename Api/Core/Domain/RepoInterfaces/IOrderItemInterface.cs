using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using EcommerceApi.Models;

namespace Api.Interfaces
{
    public interface IOrderItemInterface
    {
        public Task<List<OrderItem>?> CreateOrderItem(int orderId, List<OrderItemAddDTO> orderItemAddDTOs);
        public Task<OrderItem?>UpdateOrderItem(int orderId, int orderItemId, int Quantity);
    }
}