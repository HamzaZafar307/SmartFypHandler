using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.DTOs.Authentication
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;

        [Compare("NewPassword", ErrorMessage = "Confirm password and new password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
