using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using SportsClubUI.Models;

namespace SportsClubUI.Services
{
    public class SportsApiService
    {
        private readonly HttpClient _httpClient;

        public SportsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerInsightResponse?> GetPlayerInsightsAsync(FastApiRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5194/api/Process/player-summary",
                request
            );

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<PlayerInsightResponse>();
        }
    }
}
