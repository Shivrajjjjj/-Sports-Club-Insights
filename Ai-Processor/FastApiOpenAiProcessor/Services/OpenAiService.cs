namespace FastApiOpenAiProcessor.Services
{
    using System.Net.Http.Json;
    using FastApiOpenAiProcessor.Models;
    using Microsoft.Extensions.Options;

    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient, IOptions<OpenAiOptions> options)
        {
            _httpClient = httpClient;
            _apiKey = options.Value.ApiKey;
        }

        public async Task<string> GetInsightAsync(string prompt)
        {
            for (int attempt = 0; attempt < 5; attempt++)
            {
                var message = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                message.Headers.Add("Authorization", $"Bearer {_apiKey}");
                message.Content = JsonContent.Create(new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                new { role = "system", content = "You are a helpful sports club assistant." },
                new { role = "user", content = prompt }
            },
                    max_tokens = 200
                });

                var response = await _httpClient.SendAsync(message);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<dynamic>();
                    return result?.choices?[0]?.message?.content?.ToString() ?? string.Empty;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    // Check if server sent Retry-After header
                    if (response.Headers.TryGetValues("Retry-After", out var values) && int.TryParse(values.FirstOrDefault(), out var retryAfter))
                    {
                        await Task.Delay(retryAfter * 1000);
                    }
                    else
                    {
                        // exponential backoff
                        await Task.Delay((int)Math.Pow(2, attempt) * 1000);
                    }
                    continue;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"OpenAI API error: {response.StatusCode}, {error}");
                }
            }

            throw new HttpRequestException("OpenAI API rate limit exceeded after retries.");
        }
    }
}