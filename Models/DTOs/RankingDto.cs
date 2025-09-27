namespace SmartFYPHandler.Models.DTOs
{
    public class DepartmentRankingDto
    {
        public int Rank { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public decimal PerformanceScore { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Semester { get; set; } = string.Empty;
    }

    public class OverallRankingDto
    {
        public int Rank { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string SupervisorName { get; set; } = string.Empty;
        public decimal PerformanceScore { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Semester { get; set; } = string.Empty;
        public int Citations { get; set; }
    }

    public class RankingStatsDto
    {
        public int TotalProjects { get; set; }
        public int CompletedProjects { get; set; }
        public int InProgressProjects { get; set; }
        public decimal AveragePerformanceScore { get; set; }
        public Dictionary<string, int> ProjectsByDepartment { get; set; } = new();
        public Dictionary<string, int> ProjectsByCategory { get; set; } = new();
        public Dictionary<string, int> ProjectsByGrade { get; set; } = new();
        public List<TopPerformerDto> TopPerformingProjects { get; set; } = new();
        public List<DepartmentStatsDto> DepartmentStats { get; set; } = new();
    }

    public class TopPerformerDto
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public decimal PerformanceScore { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
    }

    public class DepartmentStatsDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int TotalProjects { get; set; }
        public decimal AverageScore { get; set; }
        public int CompletedProjects { get; set; }
        public string TopGrade { get; set; } = string.Empty;
    }
}