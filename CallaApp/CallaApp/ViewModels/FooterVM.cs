using CallaApp.Models;

namespace CallaApp.ViewModels
{
    public class FooterVM
    {
        public List<WebSiteSocial> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<MiniImage> MiniImages { get; set; }
    }
}
