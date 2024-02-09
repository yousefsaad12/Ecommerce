using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace Api.Interfaces
{
    public interface IOrderInterface
    {
        Task<Order> CreateOrder(Order order);
        Task<Order?> DeleteOrder(int orderId);
        Task<Order?> GetOrder(int orderId);
        Task<List<Order>> GetOrders();
        Task<Order> UpdateOrder(Order order, int orderItemid);

    }
}