using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class BlogDetailVM
    {
        public Blog SingleBlog { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Models.Product> Products { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<Category> Categories { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public BlogCommentVM BlogCommentVM { get; set; }
        public List<WebSiteSocial> Socials { get; set; }

    }
}
