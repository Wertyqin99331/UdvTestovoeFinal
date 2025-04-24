using VkNet;
using VkNet.Model;

namespace VkPostAnalyzer.Services;

public class VkService(string accessToken)
{
	private static readonly VkApi VkApi = new VkApi();

	public async Task<IEnumerable<string>> GetLastPostsAsync(long userId, int postsCount = 5)
	{
		await VkApi.AuthorizeAsync(new ApiAuthParams()
		{
			AccessToken = accessToken
		});

		var posts = await VkApi.Wall.GetAsync(new WallGetParams()
		{
			OwnerId = userId,
			Count = (ulong)postsCount
		});

		return posts.WallPosts
			.Where(p => p.Text != null)
			.Select(p => p.Text);
	}
}