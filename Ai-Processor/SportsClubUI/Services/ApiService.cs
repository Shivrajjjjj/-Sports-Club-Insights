using System.Net.Http.Json;
using SportsClubUI.Models;

namespace SportsClubUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlayerReport?> GetPlayerReportAsync(PlayerStats stats)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/process/player-summary", stats);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PlayerReport>();
        }
    }
}