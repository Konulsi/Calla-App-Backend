using CallaApp.Models;

namespace CallaApp.Areas.Admin.ViewModels.Product
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
    }
}
