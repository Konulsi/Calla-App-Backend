using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Decor
{
    public class DecorCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public List<IFormFile> Photos { get; set; }
    }
}
