using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using FastApiOpenAiProcessor.Services;
using FastApiOpenAiProcessor.Models;

namespace FastApiOpenAiProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly FastApiService _fastApiService;
        private readonly OpenAiService _openAiService;
        private readonly IMemoryCache _cache;

        public ProcessController(FastApiService fastApiService, OpenAiService openAiService, IMemoryCache cache)
        {
            _fastApiService = fastApiService;
            _openAiService = openAiService;
            _cache = cache;
        }

        [HttpPost("player-summary")]
        public async Task<IActionResult> GetPlayerSummary([FromBody] FastApiRequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be null.");

            var summary = await _fastApiService.GetPlayerSummaryAsync(request);

            if (summary == null)
                return BadRequest("FastAPI service returned no data.");

            var prompt =
                $"Write a short match report for {summary.Name} in {summary.Sport}. " +
                $"Highlight: {summary.Highlight}. Average score: {summary.AverageScore}.";

            string aiText;
            try
            {
                aiText = await _openAiService.GetInsightAsync(prompt);
            }
            catch
            {
                aiText =
                    $"Performance Summary: {summary.Name} played {summary.Sport}. " +
                    $"Key highlight: {summary.Highlight}. Average score: {summary.AverageScore}.";
            }

            // ✅ Store last AI report in cache
            _cache.Set("LastAIReport", aiText, TimeSpan.FromHours(1));

            return Ok(new
            {
                summary.Name,
                summary.Sport,
                summary.AverageScore,
                summary.Highlight,
                AIReport = aiText
            });
        }

        // ✅ New GET endpoint
        [HttpGet("last-report")]
        public IActionResult GetLastReport()
        {
            if (_cache.TryGetValue("LastAIReport", out string lastReport))
            {
                return Ok(new { AIReport = lastReport });
            }

            return NotFound("No AI report cached yet.");
        }
    }
}