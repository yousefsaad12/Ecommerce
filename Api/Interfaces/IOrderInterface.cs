using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using EcommerceApi.Models;

namespace Api.Interfaces
{
    public interface IOrderInterface
    {
        Task<Order?> CreateOrder(string userId);
        Task<bool?> DeleteOrder(int orderId);
        Task<Order?> GetOrder(int orderId);
        Task<List<Order>> GetOrders(string userId);
        Task<Order?> UpdateOrder(OrderItem orderItemUpdeted, int prodId, int orderId);
        Decimal TotelOrderPrice(ICollection<OrderItem> orderItem);
    }
}