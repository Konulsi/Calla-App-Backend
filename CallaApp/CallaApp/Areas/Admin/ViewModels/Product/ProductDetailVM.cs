using CallaApp.Models;

namespace CallaApp.Areas.Admin.ViewModels.Product
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; } = 5;
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string SKU { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
