using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.UserDTOS
{
    public class UserLoginDTO
    {   
        [Required(ErrorMessage = "Username must be added")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be added")]
        public string Password { get; set; }
    }
}