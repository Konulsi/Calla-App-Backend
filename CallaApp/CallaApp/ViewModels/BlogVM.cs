using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
        public List<MiniImage> MiniImages { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }

    }
}
