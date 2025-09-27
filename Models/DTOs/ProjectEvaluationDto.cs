using SmartFYPHandler.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.DTOs
{
    public class ProjectEvaluationDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public int EvaluatorId { get; set; }
        public string EvaluatorName { get; set; } = string.Empty;
        public decimal TechnicalScore { get; set; }
        public decimal InnovationScore { get; set; }
        public decimal ImplementationScore { get; set; }
        public decimal PresentationScore { get; set; }
        public decimal DocumentationScore { get; set; }
        public decimal OverallScore { get; set; }
        public string EvaluationType { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public DateTime EvaluationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateProjectEvaluationDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal TechnicalScore { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal InnovationScore { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal ImplementationScore { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal PresentationScore { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal DocumentationScore { get; set; }

        [Required]
        public EvaluationType EvaluationType { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Recommendations { get; set; } = string.Empty;

        public DateTime? EvaluationDate { get; set; }
    }

    public class UpdateProjectEvaluationDto
    {
        [Range(1, 10)]
        public decimal? TechnicalScore { get; set; }

        [Range(1, 10)]
        public decimal? InnovationScore { get; set; }

        [Range(1, 10)]
        public decimal? ImplementationScore { get; set; }

        [Range(1, 10)]
        public decimal? PresentationScore { get; set; }

        [Range(1, 10)]
        public decimal? DocumentationScore { get; set; }

        public EvaluationType? EvaluationType { get; set; }

        [StringLength(1000)]
        public string? Comments { get; set; }

        [StringLength(1000)]
        public string? Recommendations { get; set; }

        public DateTime? EvaluationDate { get; set; }
    }

    public class EvaluationScoreCalculator
    {
        // Weighted scoring: Technical 25%, Innovation 25%, Implementation 25%, Presentation 12.5%, Documentation 12.5%
        private const decimal TechnicalWeight = 0.25m;
        private const decimal InnovationWeight = 0.25m;
        private const decimal ImplementationWeight = 0.25m;
        private const decimal PresentationWeight = 0.125m;
        private const decimal DocumentationWeight = 0.125m;

        public static decimal CalculateOverallScore(
            decimal technicalScore,
            decimal innovationScore,
            decimal implementationScore,
            decimal presentationScore,
            decimal documentationScore)
        {
            return Math.Round(
                (technicalScore * TechnicalWeight) +
                (innovationScore * InnovationWeight) +
                (implementationScore * ImplementationWeight) +
                (presentationScore * PresentationWeight) +
                (documentationScore * DocumentationWeight),
                2);
        }

        public static string GetGradeFromScore(decimal overallScore)
        {
            return overallScore switch
            {
                >= 9.5m => "A+",
                >= 9.0m => "A",
                >= 8.5m => "A-",
                >= 8.0m => "B+",
                >= 7.5m => "B",
                >= 7.0m => "B-",
                >= 6.5m => "C+",
                >= 6.0m => "C",
                >= 5.5m => "C-",
                >= 5.0m => "D",
                _ => "F"
            };
        }
    }
}