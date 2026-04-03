using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.DTOs.Authentication;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IFYPProjectService
    {
        Task<PagedResult<FYPProjectDto>> GetProjectsAsync(FYPProjectSearchDto searchDto);
        Task<FYPProjectDto?> GetProjectByIdAsync(int id);
        Task<FYPProjectDto> CreateProjectAsync(CreateFYPProjectDto createProjectDto);
        Task<FYPProjectDto?> UpdateProjectAsync(int id, UpdateFYPProjectDto updateProjectDto);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<string>> GetProjectCategoriesAsync();
        Task<IEnumerable<int>> GetProjectYearsAsync();
        Task<bool> ProjectExistsAsync(int id);
        Task<IEnumerable<FYPProjectDto>> GetProjectsBySupervisorAsync(int supervisorId);
        Task<DashboardStatsDto> GetDashboardStatsAsync(int userId);
        Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryAsync(int userId);
        Task<bool> SaveSearchHistoryAsync(int userId, string query, int resultsCount);
        Task<bool> ClearSearchHistoryAsync(int userId);
        Task<IEnumerable<UserDto>> GetSupervisorsAsync();
        Task<IEnumerable<UserDto>> GetStudentsAsync();
    }
}