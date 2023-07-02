using CallaApp.Models;

namespace CallaApp.ViewModels.Cart
{
    public class CartVM
    {
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
