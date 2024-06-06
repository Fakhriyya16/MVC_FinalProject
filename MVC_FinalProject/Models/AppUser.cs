using Microsoft.AspNetCore.Identity;

namespace MVC_FinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
