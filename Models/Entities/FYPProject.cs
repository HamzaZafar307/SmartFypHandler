using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class FYPProject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Semester { get; set; } = string.Empty; // Spring/Fall

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public ProjectStatus Status { get; set; }

        public int DepartmentId { get; set; }
        public int SupervisorId { get; set; }

        [StringLength(20)]
        public string DifficultyLevel { get; set; } = string.Empty; // Easy, Medium, Hard

        [Range(0, 100)]
        public decimal PerformanceScore { get; set; }

        [StringLength(5)]
        public string FinalGrade { get; set; } = string.Empty; // A+, A, A-, B+, etc.

        public int? DepartmentRank { get; set; }
        public int? OverallRank { get; set; }

        [Range(0, int.MaxValue)]
        public int Citations { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual User Supervisor { get; set; }
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
        public virtual ICollection<ProjectEvaluation> ProjectEvaluations { get; set; } = new List<ProjectEvaluation>();
        public virtual ICollection<UserInteraction> UserInteractions { get; set; } = new List<UserInteraction>();
        public virtual ICollection<DepartmentRanking> DepartmentRankings { get; set; } = new List<DepartmentRanking>();
    }
}