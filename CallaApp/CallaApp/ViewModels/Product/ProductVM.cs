using CallaApp.Models;

namespace CallaApp.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}
