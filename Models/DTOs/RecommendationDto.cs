namespace SmartFYPHandler.Models.DTOs
{
    public class RecommendationDto
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string SupervisorName { get; set; } = string.Empty;
        public decimal PerformanceScore { get; set; }
        public string DifficultyLevel { get; set; } = string.Empty;
        public decimal RecommendationScore { get; set; }
        public string RecommendationReason { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Semester { get; set; } = string.Empty;
    }

    public class UserInteractionDto
    {
        public int ProjectId { get; set; }
        public string InteractionType { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CreateUserInteractionDto
    {
        public int ProjectId { get; set; }
        public string InteractionType { get; set; } = string.Empty;
        public int? Rating { get; set; }
    }
}