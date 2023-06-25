using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class LayoutVM
    {
        //public int BasketCount { get; set; }
        public List<WebSiteSocial> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<MiniImage> MiniImages { get; set; }

        //public List<BasketDetailVM> BasketDetailVMs { get; set; }
    }
}
