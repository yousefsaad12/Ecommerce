using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.AdminDTOS
{
    public class AdminRegsiterModel
    {
        [Required(ErrorMessage = "Admin Must have UserName")]
        public string ? UserName { get; set; }

        [Required(ErrorMessage = "Admin Must have Email")]
        [EmailAddress]
        public string ? Email { get; set; }
       

        [Required (ErrorMessage = "Admin Must have Password")]
        public string ? Password { get; set; }
    }
}