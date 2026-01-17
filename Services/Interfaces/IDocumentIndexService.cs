using SmartFYPHandler.Models.Entities;

namespace SmartFYPHandler.Services.Interfaces
{
    public class NoveltySourceSyncOptions
    {
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
    }

    public interface IDocumentIndexService
    {
        Task<int> IndexInternalFypAsync(IEnumerable<int>? projectIds = null, CancellationToken ct = default);
        Task<IReadOnlyList<IndexedDocument>> FindNearestAsync(float[] queryVec, int topK, CancellationToken ct = default);
        Task<IReadOnlyList<IndexedDocument>> FindNearestAsync(float[] queryVec, int topK, DocumentSourceType[] sources, CancellationToken ct = default);
        Task<int> IndexGitHubAsync(NoveltySourceSyncOptions options, string? query = null, CancellationToken ct = default);
        Task<int> IndexPapersAsync(NoveltySourceSyncOptions options, string? query = null, CancellationToken ct = default);
    }
}
