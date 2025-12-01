using System.Text.RegularExpressions;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class TextPreprocessor : ITextPreprocessor
    {
        private static readonly Regex NonAlphaNum = new(@"[^a-z0-9\s]", RegexOptions.Compiled);
        private static readonly Regex MultiSpace = new(@"\s+", RegexOptions.Compiled);
        private static readonly HashSet<string> Stopwords = new(new[]
        {
            "a","an","and","the","of","in","on","for","to","from","with","by","is","are","was","were","be","been","as","at","that","this","it","its","or","not","but","we","our","you","your","they","their","can","will","using","use","based"
        });

        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var lower = text.ToLowerInvariant();
            lower = NonAlphaNum.Replace(lower, " ");
            var tokens = lower.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var filtered = tokens.Where(t => t.Length > 1 && !Stopwords.Contains(t));
            var joined = string.Join(' ', filtered);
            joined = MultiSpace.Replace(joined, " ").Trim();
            return joined;
        }
    }
}

