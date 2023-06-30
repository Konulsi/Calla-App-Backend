using System.ComponentModel.DataAnnotations.Schema;

namespace CallaApp.Models
{
    public class Settings: BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public IFormFile LogoPhoto { get; set; }

        [NotMapped]
        public IFormFile PaymentPhoto { get; set; }
    }
}
