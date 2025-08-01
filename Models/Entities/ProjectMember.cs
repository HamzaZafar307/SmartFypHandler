namespace SmartFYPHandler.Models.Entities
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = "Member"; // Leader, Member
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
