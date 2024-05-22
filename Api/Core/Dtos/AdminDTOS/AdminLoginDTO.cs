using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.AdminDTOS
{
    public class AdminLoginDTO
    {
        [Required(ErrorMessage = "Admin Must have Email")]
        [EmailAddress]
        public string ? Email { get; set; }

        [Required (ErrorMessage = "Admin Must have Password")]
        public string ? Password { get; set; }
       
    }
}