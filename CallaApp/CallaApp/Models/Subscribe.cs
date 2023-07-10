namespace CallaApp.Models
{
    public class Subscribe: BaseEntity
    {
        public string Email { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
