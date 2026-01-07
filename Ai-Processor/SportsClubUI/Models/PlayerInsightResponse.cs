namespace SportsClubUI.Models
{
    public class PlayerInsightResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public double AverageScore { get; set; }
        public string Highlight { get; set; } = string.Empty;
        public string AIReport { get; set; } = string.Empty;
    }
}
