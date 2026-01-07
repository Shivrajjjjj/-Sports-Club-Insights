namespace FastApiOpenAiProcessor.Models
{
    public class FastApiResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public double AverageScore { get; set; }
        public string Highlight { get; set; } = string.Empty;
    }
}