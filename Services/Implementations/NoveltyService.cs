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

            // Trigger "Live Check" for external sources using the Title as query
            // This ensures we have relevant external matches for THIS idea
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                var syncOptions = new NoveltySourceSyncOptions(); // default options
                await _indexService.IndexGitHubAsync(syncOptions, request.Title, ct);
                await _indexService.IndexPapersAsync(syncOptions, request.Title, ct);
            }

            var embedding = await _embeddingProvider.EmbedAsync(normalized, ct);
            // Query nearest by source
            var internalDocs = await _indexService.FindNearestAsync(embedding, _options.TopK, new[] { DocumentSourceType.InternalFyp }, ct);
            var githubDocs = await _indexService.FindNearestAsync(embedding, _options.TopK, new[] { DocumentSourceType.GitHub }, ct);
            var paperDocs = await _indexService.FindNearestAsync(embedding, _options.TopK, new[] { DocumentSourceType.ResearchPaper }, ct);

            var allMatches = new List<(IndexedDocument doc, decimal sim)>();

            decimal maxInternal = 0, maxGitHub = 0, maxPapers = 0;

            foreach (var d in internalDocs)
            {
                var sim = (decimal)CosineSimilarity(embedding, d.Embedding);
                allMatches.Add((d, sim));
                if (sim > maxInternal) maxInternal = sim;
            }
            foreach (var d in githubDocs)
            {
                var sim = (decimal)CosineSimilarity(embedding, d.Embedding);
                allMatches.Add((d, sim));
                if (sim > maxGitHub) maxGitHub = sim;
            }
            foreach (var d in paperDocs)
            {
                var sim = (decimal)CosineSimilarity(embedding, d.Embedding);
                allMatches.Add((d, sim));
                if (sim > maxPapers) maxPapers = sim;
            }

            var maxWeighted =
                maxInternal * (decimal)_options.Weights.InternalFyp +
                maxGitHub * (decimal)_options.Weights.GitHub +
                maxPapers * (decimal)_options.Weights.Papers;

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
            foreach (var m in allMatches.OrderByDescending(m => m.sim).Take(_options.TopK))
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
            if (request.GitHub)
            {
                await _indexService.IndexGitHubAsync(new NoveltySourceSyncOptions { YearFrom = request.YearFrom, YearTo = request.YearTo }, null, ct);
            }
            if (request.Papers)
            {
                await _indexService.IndexPapersAsync(new NoveltySourceSyncOptions { YearFrom = request.YearFrom, YearTo = request.YearTo }, null, ct);
            }
        }

        private async Task<IdeaAnalysisResultDto> MapToResultDtoAsync(IdeaAnalysis analysis, CancellationToken ct)
        {
            var matches = await _context.IdeaMatches
                .Include(m => m.IndexedDocument) 
                .Where(m => m.IdeaAnalysisId == analysis.Id)
                .OrderBy(m => m.Rank)
                .ToListAsync(ct);

            var dto = new IdeaAnalysisResultDto
            {
                Id = analysis.Id,
                OriginalityScore = analysis.OriginalityScore,
                Category = analysis.ResultCategory.ToString(),
                MaxSimilarity = analysis.SimilarityMax,
                MaxInternalSimilarity = matches.Where(m => m.SourceType == DocumentSourceType.InternalFyp).Select(m => m.Similarity).DefaultIfEmpty(0).Max(),
                MaxGithubSimilarity = matches.Where(m => m.SourceType == DocumentSourceType.GitHub).Select(m => m.Similarity).DefaultIfEmpty(0).Max(),
                MaxPaperSimilarity = matches.Where(m => m.SourceType == DocumentSourceType.ResearchPaper).Select(m => m.Similarity).DefaultIfEmpty(0).Max(),
                TopMatches = matches.Select(m => new NoveltyMatchDto
                {
                    SourceType = m.SourceType.ToString(),
                    Title = m.Title,
                    Url = m.Url,
                    Similarity = m.Similarity,
                    Snippet = m.Snippet,
                    Year = m.IndexedDocument?.Year
                }).ToList(),
                Suggestions = BuildSuggestions(analysis, matches),
                Explanation = BuildExplanation(analysis.OriginalityScore, analysis.SimilarityMax, matches.FirstOrDefault())
            };
            return dto;
        }

        private string[] BuildSuggestions(IdeaAnalysis analysis, List<IdeaMatch> matches)
        {
            var list = new List<string>();
            var topMatch = matches.FirstOrDefault();

            // 1. Time-based suggestion
            if (topMatch != null && topMatch.IndexedDocument != null)
            {
                var age = DateTime.UtcNow.Year - topMatch.IndexedDocument.Year;
                if (age >= 5)
                {
                    list.Add($"The most similar project is from {topMatch.IndexedDocument.Year}. " +
                             $"Consider modernizing it with latest tech stacks (e.g., .NET 8, Flutter, Microservices).");
                }
                else if (age <= 1)
                {
                    list.Add("This topic is currently trending. Ensure your scope is distinct to avoid duplication.");
                }
            }

            // 2. Similarity-based pivot
            if (analysis.ResultCategory == NoveltyCategory.HighSimilarity)
            {
                list.Add("Your idea is highly similar. Try focusing on a specific niche (e.g., Healthcare, Rural Areas) instead of a generic solution.");
                list.Add("Consider a 'Hybrid' approach by integrating a secondary technology like Blockchain or IoT.");
            }
            else if (analysis.ResultCategory == NoveltyCategory.MediumSimilarity)
            {
                list.Add("To increase novelty, focus on 'Deployment' and 'Real-world Testing' which many FYPs lack.");
            }
            else
            {
                list.Add("Your idea looks original. Focus on feasibility and defining clear evaluation metrics.");
            }

            // 3. Keyword-based suggestions
            var titleLower = analysis.InputTitle.ToLower();
            if (titleLower.Contains("system") || titleLower.Contains("management"))
            {
                list.Add("Make it user-centric: Consider adding a Mobile App interface for better accessibility.");
            }
            if (titleLower.Contains("prediction") || titleLower.Contains("detection"))
            {
                list.Add("Enhancement: Explain how you will handle 'False Positives' and 'Data Privacy' in your methodology.");
            }
            if (!titleLower.Contains("cloud") && !titleLower.Contains("blockchain"))
            {
                // specific domain fusion suggestion
                if (matches.Any(m => m.Title.Contains("Machine Learning")))
                    list.Add("Differentiation: Could you optimize this model for Edge Devices (IoT) instead of Cloud?");
            }

            return list.Take(4).ToArray(); 
        }

        private string BuildExplanation(int score, decimal maxSim, IdeaMatch? topMatch)
        {
            var percentage = (maxSim * 100).ToString("F0");
            if (score == 100)
                return "Your idea appears completely unique based on our database.";
            
            var matchName = topMatch != null ? $"'{topMatch.Title}'" : "an existing project";
            return $"Your Originality Score ({score}%) is calculated by deducting the maximum similarity found ({percentage}%). " +
                   $"The most similar project found was {matchName} with a {percentage}% match.";
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
        public string EmbeddingProvider { get; set; } = "Hash"; // Hash | SBert | OpenAI (reserved)
        public bool UseStopwords { get; set; } = true;
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
