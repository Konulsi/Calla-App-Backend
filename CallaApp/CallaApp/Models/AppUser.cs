using Microsoft.AspNetCore.Identity;

namespace CallaApp.Models
{
    public class AppUser: IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
