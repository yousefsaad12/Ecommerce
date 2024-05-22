using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Core.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime OrderDT { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderItem> orderItems { get; set; }

    }
}