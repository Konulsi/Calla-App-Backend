namespace CallaApp.Areas.Admin.ViewModels.Decor
{
    public class DecorUpdateVM
    {
        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public string Image { get; set; }
        public string HoverImage { get; set; }
    }
}
