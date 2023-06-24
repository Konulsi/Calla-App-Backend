using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.About
{
    public class AboutCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
    }
}
