namespace CallaApp.Models
{
    public class Author: BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
