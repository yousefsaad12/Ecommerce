namespace Api.Core.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

    }
}