using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.Entities
{
    public class UserInteraction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public InteractionType InteractionType { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; } // Optional rating from 1-5

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual FYPProject Project { get; set; }
    }

    public enum InteractionType
    {
        Viewed = 1,
        Bookmarked = 2,
        Applied = 3,
        Rated = 4
    }
}