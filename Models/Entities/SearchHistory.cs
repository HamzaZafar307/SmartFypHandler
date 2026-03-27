using System;
using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class SearchHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Query { get; set; } = string.Empty;

        public int ResultsCount { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; } = null!;
    }
}
