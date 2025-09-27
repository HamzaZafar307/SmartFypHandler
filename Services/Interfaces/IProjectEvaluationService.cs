using SmartFYPHandler.Models.DTOs;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IProjectEvaluationService
    {
        Task<IEnumerable<ProjectEvaluationDto>> GetEvaluationsByProjectIdAsync(int projectId);
        Task<ProjectEvaluationDto?> GetEvaluationByIdAsync(int id);
        Task<ProjectEvaluationDto> CreateEvaluationAsync(int evaluatorId, CreateProjectEvaluationDto createEvaluationDto);
        Task<ProjectEvaluationDto?> UpdateEvaluationAsync(int id, int evaluatorId, UpdateProjectEvaluationDto updateEvaluationDto);
        Task<bool> DeleteEvaluationAsync(int id, int evaluatorId);
        Task<IEnumerable<ProjectEvaluationDto>> GetEvaluationsByEvaluatorAsync(int evaluatorId);
        Task<bool> CanEvaluateProjectAsync(int evaluatorId, int projectId);
        Task<decimal> CalculateProjectPerformanceScoreAsync(int projectId);
        Task UpdateProjectGradesAsync();
    }
}