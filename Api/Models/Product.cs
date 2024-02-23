using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace EcommerceApi.Models
{
    public class Product
    {
        
        public int ProductId { get; set; }

        
        [Column(TypeName ="varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName ="varchar(300)")]
        public string ? Description { get; set; }

        
        public Decimal Price { get; set; }

        
        public int StockQuantity { get; set; }
       
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }

         public ICollection<OrderItem> orderItems { get; set; }

    }
}