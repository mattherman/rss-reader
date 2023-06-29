using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RssReader.Models;

namespace RssReader.Controllers;

[Authorize]
public class FeedsController : Controller
{
	public IActionResult Index()
	{
		var dummyFeeds = new FeedsModel
		{
			Feeds = new List<Feed>
			{
				new(1, "Matthew Herman", new Uri("https://matthewherman.net/feed.xml"), 1, 3),
				new(2, "jvns.ca", new Uri("https://jvns.ca/atom.xml"), 0, 5)
			}
		};
		return View(dummyFeeds);
	}
}
