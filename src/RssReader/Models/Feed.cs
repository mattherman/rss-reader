namespace RssReader.Models;

public class Feed
{
	public Feed(
		int id,
		string name,
		Uri feedUrl,
		int unreadArticleCount = 0,
		int favoriteArticleCount = 0)
	{
		Id = id;
		Name = name;
		FeedUrl = feedUrl;
		UnreadArticleCount = unreadArticleCount;
		FavoriteArticleCount = favoriteArticleCount;
	}

	public int Id { get; }
	public string Name { get; }
	public Uri FeedUrl { get; }
	public int UnreadArticleCount { get; }
	public int FavoriteArticleCount { get; }

	public string Domain => FeedUrl.Host;
}
