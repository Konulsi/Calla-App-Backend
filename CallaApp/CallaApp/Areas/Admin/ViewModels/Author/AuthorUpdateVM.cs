using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.Author
{
    public class AuthorUpdateVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
