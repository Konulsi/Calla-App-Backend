using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Color
{
    public class ColorCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
