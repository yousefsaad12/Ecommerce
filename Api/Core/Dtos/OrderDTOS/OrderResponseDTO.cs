using Api.Core.Dtos.OrderItemDTO;

namespace Api.Core.Dtos.OrderDTOS
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDT { get; set; }
        public decimal TotalPrice { get; set; }

        public IEnumerable<OrderItemResponseDTO> ? orderItems {get; set;}
    }
}