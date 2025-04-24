namespace VkPostAnalyzer.Data.Models;

public class LetterStatistic
{
	public int Id { get; set; }
	public long UserId { get; set; }
	public string Statistics { get; set; } = null!;
	public DateTime AnalysisDate { get; set; }
}