using CallaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CallaApp.ViewModels.Contact
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
        public List<Brand> Brands { get; set; }
        public List<WebSiteSocial> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
    }
}
