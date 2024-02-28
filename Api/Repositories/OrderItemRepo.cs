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
        private readonly IProductInterface _productInterface;

        public OrderItemRepo( ApplicationDbContext context, IProductInterface productInterface)
        {
            _context = context;
            _productInterface = productInterface;
        }
        public async Task<List<OrderItem>?> CreateOrderItem(int orderId, List<OrderItemAddDTO> orderItemAddDTOs)
        {   

            List<OrderItem>orderItems = new List<OrderItem>(){};
            

            foreach(var orderIt in orderItemAddDTOs)
            {
                Product ? product = await _context.Products.FindAsync(orderIt .ProductId);

                if(product == null || product.StockQuantity < orderIt.Quantity)
                    return null;

                var OrderItem = new OrderItem ()
                {
                    OrderId = orderId,
                    ProductId = product.ProductId,
                    Quantity = orderIt.Quantity,
                    Product = product
                };

                product.StockQuantity -= orderIt.Quantity;
                await _productInterface.UpdateProductQuantity(product);
                orderItems.Add(OrderItem);
                
            }

            await _context.OrderItems.AddRangeAsync(orderItems);
                 
            await _context.SaveChangesAsync();

            return orderItems;
        }
    }
}