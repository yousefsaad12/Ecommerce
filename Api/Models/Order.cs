using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime OrderDT { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
    }
}