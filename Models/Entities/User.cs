using SmartFYPHandler.Models.DTOs.Authentication;
using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

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
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(20)]
        public string StudentId { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Project> SupervisedProjects { get; set; } = new List<Project>();
        public virtual ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
    }
}
