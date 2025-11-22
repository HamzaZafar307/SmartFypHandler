using SmartFYPHandler.Models.DTOs.Novelty;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface INoveltyService
    {
        Task<IdeaAnalysisResultDto> AnalyzeAsync(NoveltyAnalyzeRequestDto request, int userId, CancellationToken ct = default);
        Task<IdeaAnalysisResultDto?> GetAnalysisAsync(Guid id, int userId, CancellationToken ct = default);
        Task ReindexAsync(NoveltyReindexRequestDto request, CancellationToken ct = default);
    }
}

