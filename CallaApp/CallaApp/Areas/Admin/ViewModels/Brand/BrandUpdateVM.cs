using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
