using CallaApp.Helpers;
using CallaApp.Models;

namespace CallaApp.ViewModels.Blog
{
    public class BlogVM
    {
        public List<Models.Blog> Blogs { get; set; }
        public List<Models.Blog> LatesBlog { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public List<MiniImage> MiniImages { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<Models.Product> Products { get; set; }
        public Paginate<Models.Blog> PaginateDatas { get; set; }
        public BlogCommentVM BlogCommentVM { get; set; }
        public int CountProducts { get; set; }
    }
}
