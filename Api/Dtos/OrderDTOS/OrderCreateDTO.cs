using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItem;

namespace Api.Dtos.OrderDTOS
{
    public class OrderCreateDTO
    {
         public ICollection<OrderItemAddDTO> orderItems { get; set; }
    }
}