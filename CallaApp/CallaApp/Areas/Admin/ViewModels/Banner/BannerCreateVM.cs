using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Banner
{
    public class BannerCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public bool IsLarge { get; set; } = false;
    }
}
