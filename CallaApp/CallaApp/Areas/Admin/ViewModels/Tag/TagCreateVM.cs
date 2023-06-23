using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
