using CallaApp.Models;

namespace CallaApp.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public string BrandName { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<Models.Product> RelatedProducts { get; set; }
        public ProductCommentVM ProductCommentVM { get; set; }
    }
}
