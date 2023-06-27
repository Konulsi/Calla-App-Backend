using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public List<IFormFile> Photos { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
