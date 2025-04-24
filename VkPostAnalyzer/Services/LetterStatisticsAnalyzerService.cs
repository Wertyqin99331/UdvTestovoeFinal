using System.Text.Json;
using VkPostAnalyzer.Data;
using VkPostAnalyzer.Data.Models;

namespace VkPostAnalyzer.Services;

public class LetterStatisticsAnalyzerService(AppDbContext context)
{
	public async Task AnalyzeAndSaveAsync(long userId, IEnumerable<string> posts)
	{
		var combinedText = string.Join(" ", posts);
		var letterCounts = new Dictionary<char, int>();

		foreach (var c in combinedText.ToLower().Where(char.IsLetter))
		{
			letterCounts.TryAdd(c, 0);
			letterCounts[c]++;
		}

		var analysisDate = DateTime.UtcNow;
		var statistics = new LetterStatistic()
		{
			UserId = userId,
			Statistics = JsonSerializer.Serialize(letterCounts.OrderBy(p => p.Key).ToDictionary()),
			AnalysisDate = analysisDate
		};
		
		context.LetterStatistics.Add(statistics);
		await context.SaveChangesAsync();
	}
}