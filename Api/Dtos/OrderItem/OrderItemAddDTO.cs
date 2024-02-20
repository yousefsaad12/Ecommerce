using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.OrderItem
{
    public class OrderItemAddDTO
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}