namespace SmartFYPHandler.Models.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int CompletedProjects { get; set; }
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int SavedProjects { get; set; }
        public int SearchHistoryCount { get; set; }
        public int NoveltyChecksCount { get; set; }
    }
}
