using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RssReader.Models;

namespace RssReader.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		if (User.Identity is { IsAuthenticated: true })
			return RedirectToAction("Index", "Feeds");

		return View();
	}

	public IActionResult Login()
	{
		return RedirectToAction("Index", "Feeds");
	}

	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();
		return RedirectToAction("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
