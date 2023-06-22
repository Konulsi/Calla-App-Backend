using System.ComponentModel.DataAnnotations;

namespace CallaApp.Areas.Admin.ViewModels.MiniImage
{
    public class MiniImageCreateVM
    {
        public List<IFormFile> Photos { get; set; }
    }
}
