using System.Threading;
using System.Threading.Tasks;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IGeminiService
    {
        Task<string> GenerateChatResponseAsync(string systemPrompt, string userPrompt, CancellationToken ct = default);
    }
}
