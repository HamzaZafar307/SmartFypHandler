using System.Net.Http.Headers;
using System.Text.Json;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations.External
{
    public class GitHubSourceProvider : IExternalDocumentProvider
    {
        private readonly IConfiguration _config;

        public GitHubSourceProvider(IConfiguration config)
        {
            _config = config;
        }

        public DocumentSourceType SourceType => DocumentSourceType.GitHub;

        public async Task<IReadOnlyList<ExternalDocument>> FetchAsync(string query, NoveltySourceSyncOptions options, CancellationToken ct = default)
        {
            // Simple approach: search repositories by query terms and use name+description as text.
            var results = new List<ExternalDocument>();

            if (string.IsNullOrWhiteSpace(query)) return results;

            var q = Uri.EscapeDataString(query);
            var url = $"https://api.github.com/search/repositories?q={q}&sort=stars&order=desc&per_page=10";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SmartFYPHandler", "1.0"));
            var token = _config["GitHub:Token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            using var resp = await http.GetAsync(url, ct);
            if (!resp.IsSuccessStatusCode)
            {
                return results;
            }

            using var stream = await resp.Content.ReadAsStreamAsync(ct);
            using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
            if (!doc.RootElement.TryGetProperty("items", out var items) || items.ValueKind != JsonValueKind.Array)
            {
                return results;
            }

            foreach (var item in items.EnumerateArray())
            {
                var name = item.GetPropertyOrDefault("full_name") ?? item.GetPropertyOrDefault("name") ?? "";
                var htmlUrl = item.GetPropertyOrDefault("html_url") ?? "";
                var description = item.GetPropertyOrDefault("description") ?? string.Empty;
                var pushedAt = item.GetPropertyOrDefault("pushed_at");
                int? year = null;
                if (DateTime.TryParse(pushedAt, out var dt)) year = dt.Year;

                var title = string.IsNullOrWhiteSpace(name) ? (description.Length > 60 ? description[..60] : description) : name;
                var text = string.Join(' ', new[] { name, description });

                results.Add(new ExternalDocument(title, htmlUrl, year, null, null, text));
            }

            return results;
        }
    }

    internal static class JsonExt
    {
        public static string? GetPropertyOrDefault(this JsonElement el, string name)
        {
            if (el.TryGetProperty(name, out var v))
            {
                return v.ValueKind == JsonValueKind.String ? v.GetString() : v.ToString();
            }
            return null;
        }
    }
}

