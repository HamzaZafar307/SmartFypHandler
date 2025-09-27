using SmartFYPHandler.Models.DTOs;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IRankingService
    {
        Task<IEnumerable<DepartmentRankingDto>> GetDepartmentRankingsAsync(int departmentId, int? year = null, string? semester = null);
        Task<IEnumerable<OverallRankingDto>> GetOverallRankingsAsync(int? year = null, string? semester = null, int? top = null);
        Task<RankingStatsDto> GetRankingStatsAsync();
        Task UpdateRankingsAsync();
        Task<IEnumerable<DepartmentRankingDto>> GetTopProjectsByDepartmentAsync(int departmentId, int top = 10);
        Task<IEnumerable<OverallRankingDto>> GetTopProjectsOverallAsync(int top = 10);
    }
}