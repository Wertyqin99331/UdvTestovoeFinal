using Microsoft.AspNetCore.Mvc;
using VkPostAnalyzer.Services;

namespace VkPostAnalyzer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VkAnalysisController(
	VkService vkService,
	LetterStatisticsAnalyzerService letterStatisticsAnalyzerService,
	ILogger<VkAnalysisController> logger): Controller
{
	
	[HttpPost("analyze/{userId:long}")]
	public async Task<IActionResult> AnalyzePosts(long userId)
	{
		try
		{
			logger.LogInformation($"Начинаем анализ постов для пользователя {userId} в {DateTime.Now}");
                
			var posts = await vkService.GetLastPostsAsync(userId);
			await letterStatisticsAnalyzerService.AnalyzeAndSaveAsync(userId, posts);

			logger.LogInformation($"Анализ для пользователя {userId} завершен в {DateTime.Now}");
			return Ok(new { message = "Analysis completed successfully" });
		}
		catch (Exception ex)
		{
			logger.LogError(ex, $"Произошла ошибка во время анализа постов для пользователя {userId}");
			return StatusCode(500, new { error = "Произошла ошибка во время обработки запроса" });
		}
	}
}