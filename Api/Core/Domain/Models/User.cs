using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace Api.Models
{
    public class User : AppUser
    {
       [Column(TypeName ="varchar(300)")]
       public string Address { get; set; }

       public List<Order> Orders { get; set; } = new List<Order>();
       public List<Wishlist> Wishlist { get; set; } = new List<Wishlist>();
    }
}