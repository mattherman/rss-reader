namespace RssReader.Models;

public class FeedsModel
{
	public IList<Feed> Feeds { get; set; } = Array.Empty<Feed>();
}
