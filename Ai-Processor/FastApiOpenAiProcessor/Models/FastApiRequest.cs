namespace FastApiOpenAiProcessor.Models
{
    public class FastApiRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Matches { get; set; }
    }
}