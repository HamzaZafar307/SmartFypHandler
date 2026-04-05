using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations.External
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent";

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
        }

        public async Task<string> GenerateChatResponseAsync(string systemPrompt, string userPrompt, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "AI Error: Gemini API Key is not configured. Please add 'Gemini:ApiKey' to appsettings.json.";
            }

            // Merge system prompt into user prompt for maximum compatibility across API versions
            var combinedPrompt = $"{systemPrompt}\n\nSTUDENT'S REQUEST:\n{userPrompt}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[] { new { text = combinedPrompt } }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    maxOutputTokens = 800
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}?key={_apiKey}", content, ct);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"AI Error: Failed to connect to Gemini API. Status: {response.StatusCode}. Details: {error}";
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);
            
            try 
            {
                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? "AI Error: Received empty response from Gemini.";
            }
            catch (Exception ex)
            {
                return $"AI Error: Failed to parse Gemini response. {ex.Message}";
            }
        }
    }
}
