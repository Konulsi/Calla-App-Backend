using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Decor
{
    public class DecorCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile MainPhoto { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile HoverPhoto { get; set; }
    }
}
