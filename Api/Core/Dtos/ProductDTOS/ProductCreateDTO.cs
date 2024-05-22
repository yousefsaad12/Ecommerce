using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class ProductCreateDTO
    {   
        [Required]
        [MaxLength(75)]
        [MinLength(3)]
        public string  Name { get; set; }

        public string ? Description { get; set; }
        [Required]
        [Range(1, 1000000)]
        public Decimal Price { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int StockQuantity { get; set; }


    }
}