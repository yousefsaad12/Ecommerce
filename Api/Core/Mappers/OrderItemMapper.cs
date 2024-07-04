using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Models;

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
                Price = orderItem.Product.Price
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