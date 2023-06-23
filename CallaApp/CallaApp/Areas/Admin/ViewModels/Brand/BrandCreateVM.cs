using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
