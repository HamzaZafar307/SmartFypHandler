using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class IdeaAnalysis
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string InputTextHash { get; set; } = string.Empty;

        [StringLength(300)]
        public string InputTitle { get; set; } = string.Empty;

        [StringLength(4000)]
        public string? InputAbstract { get; set; }

        public int OriginalityScore { get; set; }
        public decimal SimilarityMax { get; set; }
        public NoveltyCategory ResultCategory { get; set; }
        public AnalysisStatus Status { get; set; } = AnalysisStatus.Completed;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } = DateTime.UtcNow;
        public string? ErrorMessage { get; set; }

        public virtual ICollection<IdeaMatch> Matches { get; set; } = new List<IdeaMatch>();
    }

    public class IdeaMatch
    {
        public int Id { get; set; }
        public Guid IdeaAnalysisId { get; set; }
        public int IndexedDocumentId { get; set; }
        public DocumentSourceType SourceType { get; set; }
        public decimal Similarity { get; set; }
        public int Rank { get; set; }

        [StringLength(300)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Snippet { get; set; }

        public virtual IdeaAnalysis IdeaAnalysis { get; set; }
        public virtual IndexedDocument IndexedDocument { get; set; }
    }

    public enum AnalysisStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }

    public enum NoveltyCategory
    {
        LowSimilarity = 1,
        MediumSimilarity = 2,
        HighSimilarity = 3
    }
}

