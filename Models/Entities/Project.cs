namespace SmartFYPHandler.Models.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int SupervisorId { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Category Category { get; set; }
    public virtual User Supervisor { get; set; }
    public virtual ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
}


public enum ProjectStatus
{
    Proposed = 1,
    Approved = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5
}