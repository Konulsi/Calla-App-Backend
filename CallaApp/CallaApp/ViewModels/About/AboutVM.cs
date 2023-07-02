using CallaApp.Models;

namespace CallaApp.ViewModels.About
{
    public class AboutVM
    {
        //public List<HeaderBackground> HeaderBackgrounds { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<Models.About> Abouts { get; set; }
        public List<Team> Teams { get; set; }
    }
}
