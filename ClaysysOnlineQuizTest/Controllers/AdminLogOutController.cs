 // Assuming Logger is in this namespace
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Web;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	public class AdminLogOutController : Controller
	{
		public ActionResult Index()
		{
			try
			{
				// Log the logout attempt
				Logger.LogActivity("Admin logout attempt started for user: " + Session["AdminName"]);

				// Clear all session data
				Session.Clear();
				Session.Abandon();

				// Log successful logout
				Logger.LogActivity("Admin logged out successfully.");
			}
			catch (Exception ex)
			{
				// Log error during logout
				Logger.LogError("Error occurred during admin logout: " + ex.Message);

				// Store error message in TempData to display after redirection
				TempData["ErrorMessage"] = "An error occurred while logging out: " + ex.Message;
				return RedirectToAction("Login", "Login");
			}

			// Redirect to the login page after successful logout
			return RedirectToAction("Login", "Login");
		}
	}
}
