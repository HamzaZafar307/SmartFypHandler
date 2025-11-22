namespace SmartFYPHandler.Services.Interfaces
{
    public interface IEmbeddingProvider
    {
        Task<float[]> EmbedAsync(string text, CancellationToken ct = default);
    }
}

