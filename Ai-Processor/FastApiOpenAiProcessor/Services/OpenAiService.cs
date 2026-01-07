using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using FastApiOpenAiProcessor.Models;

namespace FastApiOpenAiProcessor.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _model;

        public OpenAiService(
            HttpClient httpClient,
            IOptions<OpenAiOptions> options,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            _model = options.Value.Model ?? "gpt-4o-mini";

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", options.Value.ApiKey);
        }

        public async Task<string> GetInsightAsync(string prompt)
        {
            var cacheKey = $"openai:{prompt.GetHashCode()}";

            // ✅ Cache first
            if (_cache.TryGetValue(cacheKey, out string cached))
                return cached;

            // 🔴 Reduce retries to avoid OpenAI lock
            for (int attempt = 0; attempt < 2; attempt++)
            {
                var body = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "You are a sports club analytics assistant." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 200,
                    temperature = 0.6
                };

                var response = await _httpClient.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                    var aiText = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString() ?? string.Empty;

                    // ✅ Long cache for POC
                    _cache.Set(cacheKey, aiText, TimeSpan.FromHours(6));
                    return aiText;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    await Task.Delay(1500); // small backoff
                }
            }

            throw new HttpRequestException("OpenAI API rate limit exceeded after retries.");
        }
    }
}
