using CallaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }
        public string SKU { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int BrandId { get; set; }
        public ICollection<ProductImage> Images { get; set; }

        //[Required(ErrorMessage = "Don`t be empty")]
        public List<IFormFile> Photos { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> SizeIds { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> ColorIds { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> TagIds { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CategoryIds { get; set; }
    }
}
