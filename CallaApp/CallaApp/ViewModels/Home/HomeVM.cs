using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.ViewModels.Product;

namespace CallaApp.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Models.Product> Products { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Decor> Decors { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Team> Teams { get; set; }
        public List<Models.Blog> Blogs { get; set; }
        public List<Advertising> Advertisings { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public string Email { get; set; }
    }
}
