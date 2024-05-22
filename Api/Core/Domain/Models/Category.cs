using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }

        
        [Column(TypeName ="varchar(100)")]
        public string Name { get; set; }
        public ICollection <Product> Products { get; set; }
    }
}