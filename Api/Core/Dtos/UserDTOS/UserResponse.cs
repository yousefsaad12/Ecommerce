using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.UserDTOS
{
    public class UserResponse
    {
        public string ? UserName { get; set; }
        public string ? Email { get; set; }
        public string ? Address { get; set; }
        public string ? PhoneNumber { get; set; }
        public string ? Token { get; set; }
    }
}