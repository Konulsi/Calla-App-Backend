using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
