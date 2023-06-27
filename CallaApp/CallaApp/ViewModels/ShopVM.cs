using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class ShopVM
    {
        public List<Tag> Tags { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Brand> Brands { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
    }
}
