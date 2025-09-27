using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class DepartmentRanking
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }

        [Range(1, int.MaxValue)]
        public int RankPosition { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Semester { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal PerformanceScore { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual FYPProject Project { get; set; }
    }
}