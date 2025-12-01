using System.Net.Http.Json;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations.Embedding
{
    // Calls a local SBERT embedding endpoint (free to run). Expected response: { "embedding": [float, ...] }
    public class SentenceBertEmbeddingProvider : IEmbeddingProvider
    {
        private readonly IConfiguration _config;

        public SentenceBertEmbeddingProvider(IConfiguration config)
        {
            _config = config;
        }

        public async Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
        {
            var endpoint = _config["SBert:Endpoint"]; // e.g., http://localhost:8081/embed
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                // Fallback to empty vector if not configured
                return Array.Empty<float>();
            }

            using var http = new HttpClient();
            var req = new { text };
            using var resp = await http.PostAsJsonAsync(endpoint, req, ct);
            if (!resp.IsSuccessStatusCode)
            {
                return Array.Empty<float>();
            }

            var payload = await resp.Content.ReadFromJsonAsync<SbertResponse>(cancellationToken: ct);
            return payload?.Embedding ?? Array.Empty<float>();
        }

        private sealed class SbertResponse
        {
            public float[] Embedding { get; set; } = Array.Empty<float>();
        }
    }
}

