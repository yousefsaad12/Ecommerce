using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Models;

namespace Api.Interfaces
{
    public interface IOrderItemInterface
    {
        public Task<List<OrderItem>?> CreateOrderItem(int orderId, List<OrderItemAddDTO> orderItemAddDTOs);
        public Task<OrderItem?>UpdateOrderItem(int orderId, int orderItemId, int Quantity);
    }
}