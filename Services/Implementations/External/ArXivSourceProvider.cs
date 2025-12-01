using System.Xml.Linq;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations.External
{
    public class ArXivSourceProvider : IExternalDocumentProvider
    {
        public DocumentSourceType SourceType => DocumentSourceType.ResearchPaper;

        public async Task<IReadOnlyList<ExternalDocument>> FetchAsync(string query, NoveltySourceSyncOptions options, CancellationToken ct = default)
        {
            var list = new List<ExternalDocument>();
            if (string.IsNullOrWhiteSpace(query)) return list;

            var q = Uri.EscapeDataString(query);
            var url = $"http://export.arxiv.org/api/query?search_query=all:{q}&start=0&max_results=10";

            using var http = new HttpClient();
            var xml = await http.GetStringAsync(url, ct);

            var xdoc = XDocument.Parse(xml);
            XNamespace ns = "http://www.w3.org/2005/Atom";
            var entries = xdoc.Descendants(ns + "entry");
            foreach (var e in entries)
            {
                var title = (e.Element(ns + "title")?.Value ?? string.Empty).Trim();
                var summary = (e.Element(ns + "summary")?.Value ?? string.Empty).Trim();
                var published = e.Element(ns + "published")?.Value;
                int? year = null;
                if (DateTime.TryParse(published, out var dt)) year = dt.Year;
                var link = e.Elements(ns + "link").FirstOrDefault(l => (string?)l.Attribute("rel") == "alternate")?.Attribute("href")?.Value
                           ?? e.Element(ns + "id")?.Value
                           ?? string.Empty;

                var text = string.Join(' ', new[] { title, summary });
                list.Add(new ExternalDocument(title, link ?? string.Empty, year, null, null, text));
            }

            return list;
        }
    }
}

