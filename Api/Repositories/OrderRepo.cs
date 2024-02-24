using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using Api.Interfaces;
using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class OrderRepo : IOrderInterface
    {
        private readonly ApplicationDbContext _context;
        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<Order?> CreateOrder(string userId)
        {

                var order = new Order
            {
                UserId = userId,
                OrderDT = DateTime.Now,
            };

           await _context.Orders.AddAsync(order);
          
           await _context.SaveChangesAsync();
        
            return order;
        }

        public async Task<Order?> DeleteOrder(int orderId)
        {
            Order ? order = await GetOrder(orderId);

            if(order == null)
                return null;

            _context.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order?>  GetOrder(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetOrders(string userId)
        {
            return await _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .Include(o => o.orderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .ToListAsync();
        }

        public decimal TotelOrderPrice(ICollection<OrderItem> orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> UpdateOrder(OrderItem orderItemUpdeted, int prodId, int orderId)
        {
            Order ? order = await GetOrder(orderId);
            OrderItem ? orderItem = order.orderItems.FirstOrDefault(o => o.ProductId == prodId);

            if(orderItem == null)
                return null;

            orderItem.Quantity = orderItemUpdeted.Quantity;
            orderItem.ProductId = orderItemUpdeted.ProductId;

            await _context.SaveChangesAsync();

            return order;
        }
    }
}