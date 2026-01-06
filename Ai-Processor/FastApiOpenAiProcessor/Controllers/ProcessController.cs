namespace FastApiOpenAiProcessor.Controllers
{
	using FastApiOpenAiProcessor.Models;
	using FastApiOpenAiProcessor.Services;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("api/[controller]")]
	public class ProcessController : ControllerBase
	{
		private readonly FastApiService _fastApiService;
		private readonly OpenAiService _openAiService;

		public ProcessController(FastApiService fastApiService, OpenAiService openAiService)
		{
			_fastApiService = fastApiService;
			_openAiService = openAiService;
		}

		[HttpPost("player-summary")]
		public async Task<IActionResult> GetPlayerSummary([FromBody] FastApiRequest request)
		{
			var summary = await _fastApiService.GetPlayerSummaryAsync(request);

			if (summary == null)
				return BadRequest("FastAPI service returned no data.");

			string prompt = $"Write a short match report for {summary.Name} in {summary.Sport}. Highlight: {summary.Highlight}, Avg score: {summary.AverageScore}.";
			var aiText = await _openAiService.GetInsightAsync(prompt);

			return Ok(new
			{
				summary.Name,
				summary.Sport,
				summary.AverageScore,
				summary.Highlight,
				AIReport = aiText
			});
		}
	}
}