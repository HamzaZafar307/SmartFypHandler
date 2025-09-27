using SmartFYPHandler.Models.DTOs;

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
    }
}