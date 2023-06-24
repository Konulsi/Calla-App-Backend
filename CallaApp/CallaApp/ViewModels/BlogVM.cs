using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class BlogVM
    {
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
        public List<MiniImage> MiniImages { get; set; }
        public List<HeaderBackground> HeaderBackgrounds { get; set; }
    }
}
