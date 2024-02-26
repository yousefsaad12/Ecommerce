using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using Api.Interfaces;
using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class OrderItemRepo : IOrderItemInterface
    {   
        private readonly ApplicationDbContext _context;  
        public OrderItemRepo( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderItem>?> CreateOrderItem(int orderId, List<OrderItemAddDTO> orderItemAddDTOs)
        {   

            List<OrderItem>orderItems = new List<OrderItem>(){};

            foreach(var orderIt in orderItemAddDTOs)
            {
                Product ? product = await _context.Products.FindAsync(orderIt .ProductId);

                if(product == null)
                    return null;

                var OrderItem = new OrderItem ()
                {
                    OrderId = orderId,
                    ProductId = product.ProductId,
                    Quantity = orderIt.Quantity,
                    Product = product
                };

                orderItems.Add(OrderItem);
                
            }

            await _context.OrderItems.AddRangeAsync(orderItems);
                 
            await _context.SaveChangesAsync();

            return orderItems;
        }
    }
}