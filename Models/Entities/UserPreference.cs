using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class UserPreference
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string PreferredCategories { get; set; } = string.Empty; // JSON array

        [Required]
        public EngagementLevel EngagementLevel { get; set; }

        [Range(1, 5)]
        public decimal AverageRating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; }
    }

    public enum EngagementLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}