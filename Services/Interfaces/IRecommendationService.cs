using SmartFYPHandler.Models.DTOs;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<RecommendationDto>> GetRecommendationsAsync(int userId, int limit = 10);
        Task<UserInteractionDto> TrackInteractionAsync(int userId, CreateUserInteractionDto interactionDto);
        Task<IEnumerable<UserInteractionDto>> GetUserInteractionsAsync(int userId);
        Task UpdateUserPreferencesAsync(int userId);
        Task<IEnumerable<RecommendationDto>> GetSimilarProjectsAsync(int projectId, int limit = 5);
        Task<IEnumerable<RecommendationDto>> GetTrendingProjectsAsync(int limit = 10);
    }
}