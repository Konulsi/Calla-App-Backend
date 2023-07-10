using CallaApp.Helpers;
using CallaApp.Models;
using CallaApp.ViewModels.Product;

namespace CallaApp.ViewModels.Shop
{
    public class ShopVM
    {
        public List<Tag> Tags { get; set; }
        public List<Models.Product> Products { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Brand> Brands { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public Paginate<ProductVM> PaginateDatas { get; set; }
        public int CountProducts { get; set; }
        public int Rating { get; set; }
    }
}
