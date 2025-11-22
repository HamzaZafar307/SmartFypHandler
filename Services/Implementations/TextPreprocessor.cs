using System.Text;
using System.Text.RegularExpressions;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class TextPreprocessor : ITextPreprocessor
    {
        private static readonly Regex NonAlphaNum = new("[^a-z0-9\s]", RegexOptions.Compiled);
        private static readonly Regex MultiSpace = new("\s+", RegexOptions.Compiled);

        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var lower = text.ToLowerInvariant();
            lower = NonAlphaNum.Replace(lower, " ");
            lower = MultiSpace.Replace(lower, " ").Trim();
            return lower;
        }
    }
}

