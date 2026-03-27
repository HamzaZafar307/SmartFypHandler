using System;

namespace SmartFYPHandler.Models.DTOs
{
    public class SearchHistoryDto
    {
        public int Id { get; set; }
        public string Query { get; set; } = string.Empty;
        public int ResultsCount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
