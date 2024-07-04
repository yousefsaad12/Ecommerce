using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Core.Dtos
{
    public class ProductResponseDTO
    {   
        public int id { get; set; }
        public string ? Name { get; set; }

        public string ? Description { get; set; }

        public Decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName{ get; set; }
    }
}