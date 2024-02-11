using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Api.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApi.Models
{
    public class AppUser : IdentityUser
    {  
        [Column(TypeName ="varchar(300)")]
        public string Address { get; set; }

       public List<Order> Orders { get; set; } = new List<Order>();
       public List<Wishlist> Wishlist { get; set; } = new List<Wishlist>();

    }
}