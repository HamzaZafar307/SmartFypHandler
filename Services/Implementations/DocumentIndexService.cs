using Microsoft.EntityFrameworkCore;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class DocumentIndexService : IDocumentIndexService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITextPreprocessor _preprocessor;
        private readonly IEmbeddingProvider _embeddingProvider;

        public DocumentIndexService(ApplicationDbContext context, ITextPreprocessor preprocessor, IEmbeddingProvider embeddingProvider)
        {
            _context = context;
            _preprocessor = preprocessor;
            _embeddingProvider = embeddingProvider;
        }

        public async Task<int> IndexInternalFypAsync(IEnumerable<int>? projectIds = null, CancellationToken ct = default)
        {
            var fypQuery = _context.FYPProjects.AsQueryable();
            if (projectIds != null)
            {
                var ids = projectIds.ToList();
                if (ids.Count > 0)
                    fypQuery = fypQuery.Where(p => ids.Contains(p.Id));
            }

            var fypList = await fypQuery.ToListAsync(ct);
            int indexed = 0;
            foreach (var p in fypList)
            {
                var existing = await _context.IndexedDocuments
                    .FirstOrDefaultAsync(d => d.SourceType == DocumentSourceType.InternalFyp && d.SourceEntityId == p.Id, ct);

                var text = $"{p.Title} {p.Description} {p.Category} {p.Semester}";
                var norm = _preprocessor.Normalize(text);
                var emb = await _embeddingProvider.EmbedAsync(norm, ct);

                if (existing == null)
                {
                    var doc = new IndexedDocument
                    {
                        SourceType = DocumentSourceType.InternalFyp,
                        SourceEntityId = p.Id,
                        Title = p.Title,
                        Url = string.Empty,
                        Year = p.Year,
                        DepartmentId = p.DepartmentId,
                        Category = p.Category,
                        Embedding = emb,
                        MetadataJson = string.Empty,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.IndexedDocuments.Add(doc);
                }
                else
                {
                    existing.Title = p.Title;
                    existing.Year = p.Year;
                    existing.DepartmentId = p.DepartmentId;
                    existing.Category = p.Category;
                    existing.Embedding = emb;
                    existing.UpdatedAt = DateTime.UtcNow;
                }

                indexed++;
            }

            await _context.SaveChangesAsync(ct);
            return indexed;
        }

        public async Task<IReadOnlyList<IndexedDocument>> FindNearestAsync(float[] queryVec, int topK, CancellationToken ct = default)
        {
            // For MVP: only InternalFyp documents
            var docs = await _context.IndexedDocuments
                .Where(d => d.SourceType == DocumentSourceType.InternalFyp)
                .ToListAsync(ct);

            var scored = new List<(IndexedDocument doc, decimal sim)>();
            foreach (var d in docs)
            {
                var sim = CosineSimilarity(queryVec, d.Embedding);
                scored.Add((d, (decimal)sim));
            }

            return scored
                .OrderByDescending(s => s.sim)
                .Take(topK)
                .Select(s => s.doc)
                .ToList();
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
}

