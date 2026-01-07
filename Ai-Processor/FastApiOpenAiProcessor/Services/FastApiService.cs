namespace FastApiOpenAiProcessor.Services
{
    using System.Net.Http.Json;
    using FastApiOpenAiProcessor.Models;

    public class FastApiService
    {
        private readonly HttpClient _httpClient;

        public FastApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FastApiResponse?> GetPlayerSummaryAsync(FastApiRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:8000/data", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FastApiResponse>();
        }
    }
}