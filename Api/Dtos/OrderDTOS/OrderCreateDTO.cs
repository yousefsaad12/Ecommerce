using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.OrderDTOS
{
    public class OrderCreateDTO
    {
        public int ProdId { get; set; }
        public int Quantity { get; set; }
    }
}