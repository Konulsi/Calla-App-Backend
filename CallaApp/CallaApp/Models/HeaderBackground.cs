using System.ComponentModel.DataAnnotations.Schema;

namespace CallaApp.Models
{
    public class HeaderBackground: BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public IFormFile BackgroundPhoto { get; set; }
    }
}
