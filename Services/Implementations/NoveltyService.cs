using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs.Novelty;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class NoveltyService : INoveltyService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITextPreprocessor _preprocessor;
        private readonly IEmbeddingProvider _embeddingProvider;
        private readonly IDocumentIndexService _indexService;
        private readonly IMemoryCache _cache;
        private readonly NoveltyOptions _options;

        public NoveltyService(
            ApplicationDbContext context,
            ITextPreprocessor preprocessor,
            IEmbeddingProvider embeddingProvider,
            IDocumentIndexService indexService,
            IMemoryCache cache,
            NoveltyOptions options)
        {
            _context = context;
            _preprocessor = preprocessor;
            _embeddingProvider = embeddingProvider;
            _indexService = indexService;
            _cache = cache;
            _options = options;
        }

        public async Task<IdeaAnalysisResultDto> AnalyzeAsync(NoveltyAnalyzeRequestDto request, int userId, CancellationToken ct = default)
        {
            var input = string.Join(" ", new[] { request.Title ?? string.Empty, request.Abstract ?? string.Empty });
            var normalized = _preprocessor.Normalize(input);
            var key = $"novelty::{userId}::{Hash(normalized)}";

            if (_cache.TryGetValue<IdeaAnalysisResultDto>(key, out var cached) && cached != null)
                return cached;

            // Try to reuse completed identical analysis
            var inputHash = Hash(normalized);
            var existing = await _context.IdeaAnalyses
                .Include(a => a.Matches)
                .Where(a => a.UserId == userId && a.InputTextHash == inputHash && a.Status == AnalysisStatus.Completed)
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync(ct);

            if (existing != null)
            {
                var fromDb = await MapToResultDtoAsync(existing, ct);
                _cache.Set(key, fromDb, TimeSpan.FromMinutes(_options.CacheMinutes));
                return fromDb;
            }

            // Ensure internal index exists/updated at least once (no-op if present)
            await _indexService.IndexInternalFypAsync(null, ct);

            var embedding = await _embeddingProvider.EmbedAsync(normalized, ct);
            var nearest = await _indexService.FindNearestAsync(embedding, _options.TopK, ct);

            // Compute similarity scores (only internal FYPs for MVP)
            var matches = new List<(IndexedDocument doc, decimal sim)>();
            foreach (var d in nearest)
            {
                var sim = CosineSimilarity(embedding, d.Embedding);
                matches.Add((d, (decimal)sim));
            }

            var maxInternal = matches.Count > 0 ? matches.Max(m => m.sim) : 0m;
            var maxWeighted = maxInternal * (decimal)_options.Weights.InternalFyp;
            var originality = (int)Math.Clamp((1m - maxWeighted) * 100m, 0m, 100m);
            var category = maxWeighted >= (decimal)_options.Similarity.High
                ? NoveltyCategory.HighSimilarity
                : maxWeighted >= (decimal)_options.Similarity.Medium
                    ? NoveltyCategory.MediumSimilarity
                    : NoveltyCategory.LowSimilarity;

            var analysis = new IdeaAnalysis
            {
                UserId = userId,
                InputTextHash = inputHash,
                InputTitle = request.Title ?? string.Empty,
                InputAbstract = request.Abstract,
                OriginalityScore = originality,
                SimilarityMax = maxInternal,
                ResultCategory = category,
                Status = AnalysisStatus.Completed,
                CreatedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow
            };

            _context.IdeaAnalyses.Add(analysis);
            await _context.SaveChangesAsync(ct);

            // Persist top matches
            int rank = 1;
            foreach (var m in matches.OrderByDescending(m => m.sim).Take(_options.TopK))
            {
                _context.IdeaMatches.Add(new IdeaMatch
                {
                    IdeaAnalysisId = analysis.Id,
                    IndexedDocumentId = m.doc.Id,
                    SourceType = m.doc.SourceType,
                    Similarity = m.sim,
                    Rank = rank++,
                    Title = m.doc.Title,
                    Url = m.doc.Url,
                    Snippet = null
                });
            }
            await _context.SaveChangesAsync(ct);

            var dto = await MapToResultDtoAsync(analysis, ct);
            _cache.Set(key, dto, TimeSpan.FromMinutes(_options.CacheMinutes));
            return dto;
        }

        public async Task<IdeaAnalysisResultDto?> GetAnalysisAsync(Guid id, int userId, CancellationToken ct = default)
        {
            var analysis = await _context.IdeaAnalyses
                .Include(a => a.Matches)
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId, ct);
            if (analysis == null) return null;
            return await MapToResultDtoAsync(analysis, ct);
        }

        public async Task ReindexAsync(NoveltyReindexRequestDto request, CancellationToken ct = default)
        {
            if (request.InternalFyp)
            {
                await _indexService.IndexInternalFypAsync(null, ct);
            }
            // GitHub/Papers providers can be plugged here later
        }

        private async Task<IdeaAnalysisResultDto> MapToResultDtoAsync(IdeaAnalysis analysis, CancellationToken ct)
        {
            var matches = await _context.IdeaMatches
                .Where(m => m.IdeaAnalysisId == analysis.Id)
                .OrderBy(m => m.Rank)
                .ToListAsync(ct);

            var dto = new IdeaAnalysisResultDto
            {
                Id = analysis.Id,
                OriginalityScore = analysis.OriginalityScore,
                Category = analysis.ResultCategory.ToString(),
                MaxSimilarity = analysis.SimilarityMax,
                TopMatches = matches.Select(m => new NoveltyMatchDto
                {
                    SourceType = m.SourceType.ToString(),
                    Title = m.Title,
                    Url = m.Url,
                    Similarity = m.Similarity,
                    Snippet = m.Snippet
                }).ToList(),
                Suggestions = BuildSuggestions(analysis)
            };
            return dto;
        }

        private string[] BuildSuggestions(IdeaAnalysis analysis)
        {
            var list = new List<string>();
            if (analysis.ResultCategory == NoveltyCategory.HighSimilarity)
            {
                list.Add("Idea is very similar; consider changing scope or domain.");
                list.Add("Add unique features such as domain fusion (e.g., blockchain integration).");
            }
            else if (analysis.ResultCategory == NoveltyCategory.MediumSimilarity)
            {
                list.Add("Idea shares elements with others; refine objectives or target niche use-cases.");
                list.Add("Consider localization, cultural adaptation, or wearable integration.");
            }
            else
            {
                list.Add("Idea appears original; proceed and strengthen methodology.");
            }
            return list.ToArray();
        }

        private static string Hash(string text)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        private static float CosineSimilarity(IReadOnlyList<float> a, IReadOnlyList<float> b)
        {
            if (a.Count == 0 || b.Count == 0 || a.Count != b.Count) return 0f;
            float dot = 0, na = 0, nb = 0;
            for (int i = 0; i < a.Count; i++)
            {
                var va = a[i];
                var vb = b[i];
                dot += va * vb;
                na += va * va;
                nb += vb * vb;
            }
            var denom = MathF.Sqrt(na) * MathF.Sqrt(nb);
            return denom > 0 ? dot / denom : 0f;
        }
    }

    public class NoveltyOptions
    {
        public SimilarityThresholds Similarity { get; set; } = new() { High = 0.8, Medium = 0.5 };
        public SourceWeights Weights { get; set; } = new();
        public int TopK { get; set; } = 10;
        public int CacheMinutes { get; set; } = 60;
    }

    public class SimilarityThresholds
    {
        public double High { get; set; }
        public double Medium { get; set; }
    }

    public class SourceWeights
    {
        public double InternalFyp { get; set; } = 1.0; // MVP: only internal
        public double GitHub { get; set; } = 0.25;
        public double Papers { get; set; } = 0.25;
    }
}

