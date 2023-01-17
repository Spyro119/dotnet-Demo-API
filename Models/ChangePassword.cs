using System.ComponentModel.DataAnnotations;


namespace Demo_API.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter your current password")]
        public string? OldPassword { get; set; }
        [Required(ErrorMessage = "Please enter your new password")]
        public string? NewPassword { get; set; }
    }
}