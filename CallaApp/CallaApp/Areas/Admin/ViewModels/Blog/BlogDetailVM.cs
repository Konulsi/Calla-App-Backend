using CallaApp.Models;

namespace CallaApp.Areas.Admin.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public List<BlogImage> BlogImages { get; set; }
    }
}
