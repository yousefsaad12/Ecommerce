using Api.Core.Dtos.OrderDTOS;
using Api.Core.Models;

namespace Api.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDTO ToOrderResponseDTO(this Order order)
        {
            return new OrderResponseDTO()
            {
                UserName = order.User.UserName,
                OrderDT = order.OrderDT,
                OrderId = order.OrderId,
                orderItems = order.orderItems.Select(oi => oi.ToOrderItemResponseDTO()),
                TotalPrice = order.orderItems.Select(oi => oi.ToOrderItemResponseDTO()).Sum(oi => oi.Price * oi.Quantity)
            };
        }

        
    }
}