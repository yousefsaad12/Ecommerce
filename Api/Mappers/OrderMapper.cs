using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderDTOS;
using Api.Models;
using EcommerceApi.Models;

namespace Api.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDTO ToOrderResponseDTO(this Order order)
        {
            return new OrderResponseDTO()
            {
                OrderDT = order.OrderDT,
                OrderId = order.OrderId,
                orderItems = order.orderItems.Select(oi => oi.ToOrderItemResponseDTO())
            };
        }

        
    }
}