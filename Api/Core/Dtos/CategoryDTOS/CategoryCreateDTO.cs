using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.CategoryDTOS
{
    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(75)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}