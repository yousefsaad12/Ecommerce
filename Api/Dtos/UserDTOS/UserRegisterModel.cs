using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.UserDTOS
{
    public class UserRegisterModel
    {
        
        [Required(ErrorMessage = "User Must have UserName")]
        public string ? UserName { get; set; }

        [Required(ErrorMessage = "User Must have Email")]
        [EmailAddress]
        public string ? Email { get; set; }

        [Required(ErrorMessage = "User Must have Address")]
        public string ? Address { get; set; }
        public string ? PhoneNumber { get; set; }

        [Required (ErrorMessage = "User Must have Password")]
        public string ? Password { get; set; }
    }
}