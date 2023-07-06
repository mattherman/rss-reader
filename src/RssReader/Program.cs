using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.HttpOverrides;
using RssReader.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var googleAuthConfiguration =
	builder.Configuration
		.GetSection("GoogleAuth")
		.Get<GoogleAuthOptions>();

if (googleAuthConfiguration?.ClientId is null)
	throw new Exception("ClientId must be configured for Google authentication");
if (googleAuthConfiguration?.ClientSecret is null)
	throw new Exception("ClientSecret must be configured for Google authentication");

builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultScheme =
			CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme =
			GoogleDefaults.AuthenticationScheme;
	})
	.AddCookie()
	.AddGoogle(options =>
	{
		options.ClientId = googleAuthConfiguration.ClientId;
		options.ClientSecret = googleAuthConfiguration.ClientSecret;
	});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
	options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHttpsRedirection();
	app.UseForwardedHeaders();
	app.UsePathBase("/rss-reader");

	app.UseExceptionHandler("/Home/Error");

	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
