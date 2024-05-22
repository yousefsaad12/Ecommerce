using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.OrderItemDTO
{
    public class OrderItemUpdateDTO
    {
        [Required (ErrorMessage="Quantity is must be added")]
        [Range(1, 5000,ErrorMessage =("Quantity must between 1 to 5000"))]
          public int Quantity { get; set; }

        [Required (ErrorMessage="Product id is must be added")]
          public int ProductId { get; set; }
    }
}