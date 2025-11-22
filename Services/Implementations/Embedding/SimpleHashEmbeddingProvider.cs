using System.Security.Cryptography;
using System.Text;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations.Embedding
{
    // Lightweight, deterministic embedding for development/testing without external dependencies.
    // Produces a fixed-length vector by hashing tokens into buckets.
    public class SimpleHashEmbeddingProvider : IEmbeddingProvider
    {
        private const int Dim = 256;

        public Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
        {
            var vec = new float[Dim];
            if (string.IsNullOrWhiteSpace(text)) return Task.FromResult(vec);

            var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var token in tokens)
            {
                var h = HashToUInt(token);
                var idx = (int)(h % (uint)Dim);
                vec[idx] += 1f;
            }

            // L2 normalize
            var norm = MathF.Sqrt(vec.Sum(v => v * v));
            if (norm > 0)
            {
                for (int i = 0; i < vec.Length; i++) vec[i] /= norm;
            }

            return Task.FromResult(vec);
        }

        private static uint HashToUInt(string s)
        {
            // FNV-1a 32-bit
            unchecked
            {
                uint hash = 2166136261;
                foreach (char c in s)
                {
                    hash ^= c;
                    hash *= 16777619;
                }
                return hash;
            }
        }
    }
}

