namespace FastApiOpenAiProcessor.Models
{
    public class OpenAiRequest
    {
        public string Prompt { get; set; } = string.Empty;
        public int MaxTokens { get; set; } = 150;
    }
}