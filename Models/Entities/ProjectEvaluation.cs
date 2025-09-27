using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class ProjectEvaluation
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public int EvaluatorId { get; set; }

        [Range(1, 10)]
        public decimal TechnicalScore { get; set; }

        [Range(1, 10)]
        public decimal InnovationScore { get; set; }

        [Range(1, 10)]
        public decimal ImplementationScore { get; set; }

        [Range(1, 10)]
        public decimal PresentationScore { get; set; }

        [Range(1, 10)]
        public decimal DocumentationScore { get; set; }

        public decimal OverallScore { get; set; } // Calculated field

        [Required]
        public EvaluationType EvaluationType { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Recommendations { get; set; } = string.Empty;

        public DateTime EvaluationDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual FYPProject Project { get; set; }
        public virtual User Evaluator { get; set; }
    }

    public enum EvaluationType
    {
        Interim = 1,
        Final = 2,
        Defense = 3
    }
}