using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItem;
using EcommerceApi.Models;

namespace Api.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemResponseDTO ToOrderItemResponseDTO(this OrderItem orderItem)
        {
            return new OrderItemResponseDTO ()
            {
                Price = orderItem.Price,
                ProductName = orderItem.Product.Name,
                Quantity = orderItem.Quantity
            };
        }
    }
}