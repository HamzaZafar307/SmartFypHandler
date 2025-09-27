using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class ProjectCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // ML, IoT, Blockchain, Web Development, etc.

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<FYPProject> FYPProjects { get; set; } = new List<FYPProject>();
        public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
    }
}