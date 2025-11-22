using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class IndexedDocument
    {
        public int Id { get; set; }

        [Required]
        public DocumentSourceType SourceType { get; set; }

        // For internal FYPs, this links to FYPProject.Id
        public int? SourceEntityId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;

        public int? Year { get; set; }
        public int? DepartmentId { get; set; }

        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        // Serialized embedding vector (JSON). Mapped via value converter.
        [Required]
        public float[] Embedding { get; set; } = Array.Empty<float>();

        public string MetadataJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum DocumentSourceType
    {
        InternalFyp = 1,
        GitHub = 2,
        ResearchPaper = 3
    }
}

