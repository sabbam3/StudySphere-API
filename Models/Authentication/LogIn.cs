using System.ComponentModel.DataAnnotations;

namespace StudySphere_API.Models.Authentication
{
    public class LogIn
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
