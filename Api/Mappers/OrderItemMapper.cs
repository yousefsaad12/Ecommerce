using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using EcommerceApi.Models;

namespace Api.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemResponseDTO ToOrderItemResponseDTO(this OrderItem orderItem)
        {
            return new OrderItemResponseDTO ()
            {
                
                ProductName = orderItem.Product.Name,
                Quantity = orderItem.Quantity,
                Price = orderItem.Product.Price * orderItem.Quantity,
                category = orderItem.Product.Category.Name,
            };
        }

        public static OrderItem ToOrderItem(this OrderItemAddDTO orderItem, int prodId, int orderId)
        {
            return new OrderItem ()
            {
                ProductId = prodId,
                Quantity = orderItem.Quantity,
                OrderId = orderId
            };
        }
    }
}