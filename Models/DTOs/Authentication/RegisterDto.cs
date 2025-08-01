using SmartFYPHandler.Models.DTOs.Authentication;
using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.DTOs.Authentication;

public class RegisterDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [StringLength(20)]
    public string StudentId { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;
}



public enum UserRole
{
    Student = 1,
    Teacher = 2,
    Admin = 3
}