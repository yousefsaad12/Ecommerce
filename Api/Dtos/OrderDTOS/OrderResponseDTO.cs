using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace Api.Dtos.OrderDTOS
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDT { get; set; }
        public decimal TotalPrice { get; set; }

        public ICollection<OrderItem> orderItems {get; set;}
    }
}