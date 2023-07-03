namespace CallaApp.Models
{
    public class Wishlist
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<WishlistProduct> CartProducts { get; set; }
    }
}
