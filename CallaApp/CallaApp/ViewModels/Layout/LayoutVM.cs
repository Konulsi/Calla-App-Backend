using CallaApp.Models;
using CallaApp.ViewModels.Cart;
using CallaApp.ViewModels.Shop;

namespace CallaApp.ViewModels.Layout
{
    public class LayoutVM
    {
        public int BasketCount { get; set; }
        public int WishlistCount { get; set; }
        public List<WebSiteSocial> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<MiniImage> MiniImages { get; set; }

        public IEnumerable<CartVM> CartVMs { get; set; }
        public IEnumerable<ShopVM> ShopVMs { get; set; }

        //public string Name { get; set; }
        //public string Image { get; set; }
        //public decimal Price { get; set; }
        //public int ProCount { get; set; }
        //public decimal Subtotal { get; set; }
    }
}
