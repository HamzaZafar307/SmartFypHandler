namespace SmartFYPHandler.Models.DTOs.Novelty
{
    public class NoveltyAnalyzeRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Abstract { get; set; }
        public bool Async { get; set; } = false;
    }

    public class NoveltyMatchDto
    {
        public string SourceType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public decimal Similarity { get; set; }
        public string? Snippet { get; set; }
        public int? Year { get; set; }
        public string? Department { get; set; }
        public string? Category { get; set; }
    }

    public class IdeaAnalysisResultDto
    {
        public Guid Id { get; set; }
        public int OriginalityScore { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal MaxSimilarity { get; set; }
        public IReadOnlyList<NoveltyMatchDto> TopMatches { get; set; } = new List<NoveltyMatchDto>();
        public string[] Suggestions { get; set; } = Array.Empty<string>();
    }

    public class NoveltyReindexRequestDto
    {
        public bool InternalFyp { get; set; } = true;
        public bool GitHub { get; set; } = false;
        public bool Papers { get; set; } = false;
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
    }
}

