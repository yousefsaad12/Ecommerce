using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Models;
using Api.Infrastructer.Data;
using Microsoft.EntityFrameworkCore;
using Api.Core.Domain;

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

        

        public async Task<OrderItem?> UpdateOrderItem(int orderId, int prodId, int Quantity)
        {
            Product ? product = await _productInterface.GetProductById(prodId);

            if(product == null) return null;

            OrderItem orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == prodId);

            if(orderItem.Quantity > Quantity)
                product.StockQuantity += (orderItem.Quantity - Quantity);

            else if(orderItem.Quantity < Quantity)
                 product.StockQuantity -= Quantity;

            orderItem.Quantity = Quantity;

            await _productInterface.UpdateProductQuantity(product);
            _context.OrderItems.Update(orderItem);

            await _context.SaveChangesAsync();

            return orderItem;

        }

        Task<OrderItem?> IOrderItemInterface.UpdateOrderItem(int orderId, int orderItemId, int Quantity)
        {
            throw new NotImplementedException();
        }
    }
}