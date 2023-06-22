using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallaApp.Areas.Admin.ViewModels.MiniImage
{
    public class MiniImageUpdateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}
