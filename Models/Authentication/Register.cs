using System.ComponentModel.DataAnnotations;

namespace StudySphere_API.Models.Authentication
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
