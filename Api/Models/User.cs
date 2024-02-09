using System.ComponentModel.DataAnnotations.Schema;
using Api.Models;

namespace EcommerceApi.Models
{
    public class User
    {    
       
       public int UserId {get; set;}

       [Column(TypeName ="varchar(100)")]
       
       public string UserName { get; set; }
       
       [Column(TypeName ="varchar(200)")]
       
       public string Password { get; set; }

       [Column(TypeName ="varchar(200)")]
       
       public string Email  { get; set; }

       [Column(TypeName ="varchar(100)")]
       public string FName { get; set; }

       [Column(TypeName ="varchar(100)")]
       public string LName { get; set; }

       [Column(TypeName ="varchar(200)")]
       public string Address { get; set; }

       public ICollection<Order> Orders { get; set; }
       public ICollection<Wishlist> Wishlist { get; set; }

       
    }
}