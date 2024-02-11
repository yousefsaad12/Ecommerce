using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.AddAsync(order);
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

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.orderItems).ToListAsync();
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