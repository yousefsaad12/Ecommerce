using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Core.Models
{
    public class User : AppUser
    {
       [Column(TypeName ="varchar(300)")]
       public string Address { get; set; }

       public List<Order> Orders { get; set; } = new List<Order>();
       public List<Wishlist> Wishlist { get; set; } = new List<Wishlist>();
    }
}