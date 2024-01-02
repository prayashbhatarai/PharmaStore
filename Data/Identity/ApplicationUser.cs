using Microsoft.AspNetCore.Identity;

namespace PharmaStore.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public String Address { get; set; } = string.Empty;
    }
}
