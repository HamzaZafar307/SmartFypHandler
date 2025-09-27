using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class ProjectMember
    {
        public int Id { get; set; }

        public int? ProjectId { get; set; } // For legacy projects
        public int? FYPProjectId { get; set; } // For FYP projects
        public int UserId { get; set; }

        [Required]
        public MemberRole Role { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Project? Project { get; set; }
        public virtual FYPProject? FYPProject { get; set; }
        public virtual User User { get; set; }
    }

    public enum MemberRole
    {
        TeamLead = 1,
        Member = 2
    }
}
