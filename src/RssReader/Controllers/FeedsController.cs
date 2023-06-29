using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RssReader.Controllers;

[Authorize]
public class FeedsController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
