using System.ComponentModel.DataAnnotations;

namespace Demo_API.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}