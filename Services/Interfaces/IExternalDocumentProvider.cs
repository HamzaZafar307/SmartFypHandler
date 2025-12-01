using SmartFYPHandler.Models.Entities;

namespace SmartFYPHandler.Services.Interfaces
{
    public record ExternalDocument(
        string Title,
        string Url,
        int? Year,
        int? DepartmentId,
        string? Category,
        string Text
    );

    public interface IExternalDocumentProvider
    {
        DocumentSourceType SourceType { get; }
        Task<IReadOnlyList<ExternalDocument>> FetchAsync(string query, NoveltySourceSyncOptions options, CancellationToken ct = default);
    }
}

