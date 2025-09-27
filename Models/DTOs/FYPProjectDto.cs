using SmartFYPHandler.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartFYPHandler.Models.DTOs
{
    public class FYPProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Semester { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty;
        public decimal PerformanceScore { get; set; }
        public string FinalGrade { get; set; } = string.Empty;
        public int? DepartmentRank { get; set; }
        public int? OverallRank { get; set; }
        public int Citations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProjectMemberDto> ProjectMembers { get; set; } = new List<ProjectMemberDto>();
    }

    public class ProjectMemberDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
    }

    public class CreateFYPProjectDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(2020, 2030)]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Semester { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int SupervisorId { get; set; }

        [StringLength(20)]
        public string DifficultyLevel { get; set; } = "Medium";

        public List<int> StudentIds { get; set; } = new List<int>();
    }

    public class UpdateFYPProjectDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(2020, 2030)]
        public int? Year { get; set; }

        [StringLength(20)]
        public string? Semester { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        public ProjectStatus? Status { get; set; }

        public int? DepartmentId { get; set; }

        public int? SupervisorId { get; set; }

        [StringLength(20)]
        public string? DifficultyLevel { get; set; }

        [Range(0, 100)]
        public decimal? PerformanceScore { get; set; }

        [StringLength(5)]
        public string? FinalGrade { get; set; }

        [Range(0, int.MaxValue)]
        public int? Citations { get; set; }
    }

    public class FYPProjectSearchDto
    {
        public string? Title { get; set; }
        public string? Category { get; set; }
        public int? DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public int? Year { get; set; }
        public string? Semester { get; set; }
        public ProjectStatus? Status { get; set; }
        public string? DifficultyLevel { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}