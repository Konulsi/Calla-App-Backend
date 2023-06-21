using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Advertising
{
    public class AdvertisingUpdateVM
    {
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Icon { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
    }
}
