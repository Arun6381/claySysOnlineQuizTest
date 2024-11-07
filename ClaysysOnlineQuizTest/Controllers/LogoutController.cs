using System;
using System.Web;
using System.Web.Mvc;
using ClaysysOnlineQuizTest.Utitlities; // Assuming Logger is in this namespace

namespace ClaysysOnlineQuizTest.Controllers
{
	public class LogoutController : Controller
	{
		public ActionResult Index()
		{
			try
			{
				// Log the logout attempt
				Logger.LogActivity("Logout attempt started.");

				// Clear all session data
				Session.Clear();
				Session.Abandon();

				// Expire authentication cookies
				if (Request.Cookies["ASP.NET_SessionId"] != null)
				{
					var sessionCookie = new HttpCookie("ASP.NET_SessionId") { Expires = DateTime.Now.AddDays(-1) };
					Response.Cookies.Add(sessionCookie);
					Logger.LogActivity("Session cookie expired.");
				}

				if (Request.Cookies[".ASPXAUTH"] != null)
				{
					var authCookie = new HttpCookie(".ASPXAUTH") { Expires = DateTime.Now.AddDays(-1) };
					Response.Cookies.Add(authCookie);
					Logger.LogActivity(".ASPXAUTH cookie expired.");
				}

				// Set no-cache headers to prevent back navigation after logout
				Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
				Response.Cache.SetCacheability(HttpCacheability.NoCache);
				Response.Cache.SetNoStore();

				// Log successful logout
				Logger.LogActivity("User logged out successfully.");
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred during logout: " + ex.Message);

				// Store error message in TempData to display after redirection
				TempData["ErrorMessage"] = "An error occurred while logging out: " + ex.Message;
				return RedirectToAction("Login", "Login");
			}

			// Redirect to the login page after successful logout
			return RedirectToAction("Login", "Login");
		}
	}
}
