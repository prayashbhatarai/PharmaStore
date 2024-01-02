using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Modules.Dtos
{
    public class LoginDto
    {
        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
