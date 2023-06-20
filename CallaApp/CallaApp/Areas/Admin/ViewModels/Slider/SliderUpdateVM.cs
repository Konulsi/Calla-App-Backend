using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
