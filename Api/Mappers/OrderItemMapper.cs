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
                Quantity = orderItem.Quantity
            };
        }

        public static OrderItem ToOrderItem(this OrderItemAddDTO orderItem, Product product)
        {
            return new OrderItem ()
            {
                ProductId = product.ProductId,
                Quantity = orderItem.Quantity
            };
        }
    }
}