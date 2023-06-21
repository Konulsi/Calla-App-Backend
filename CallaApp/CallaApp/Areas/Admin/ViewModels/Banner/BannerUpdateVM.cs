namespace CallaApp.Areas.Admin.ViewModels.Banner
{
    public class BannerUpdateVM
    {
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        public bool IsLarge { get; set; } = false;
    }
}
