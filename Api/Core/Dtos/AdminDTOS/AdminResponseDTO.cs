using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.AdminDTOS
{
    public class AdminResponseDTO
    {
        public string ? UserName { get; set; }
        public string ? Email { get; set; }
        public string ? Token { get; set; }
    }
}