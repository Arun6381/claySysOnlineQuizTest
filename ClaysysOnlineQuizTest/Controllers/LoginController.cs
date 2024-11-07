using System;
using System.Linq;
using System.Web.Mvc;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.DataAccessLayer;
using BCrypt.Net;
using ClaysysOnlineQuizTest.Utitlities; // Assuming Logger is in this namespace

namespace ClaysysOnlineQuizTest.Controllers
{
	public class LoginController : Controller
	{
		private SignUpDataAccess userDataAccess = new SignUpDataAccess();

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(SignUp model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					SignUp user = userDataAccess.GetByEmail(model.EmailId);

					if (user != null)
					{
						bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.UserPassword, user.UserPassword);

						if (isPasswordValid)
						{
							// Log successful login
							Logger.LogActivity($"User {user.EmailId} logged in successfully with role {user.Role}.");

							Session["EmailID"] = user.EmailId;
							Session["UserID"] = user.UserID;
							Session["UserName"] = user.FirstName;
							Session["UserRole"] = user.Role;

							if (user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
							{
								return RedirectToAction("Index", "Admin");
							}
							else if (user.Role.Equals("User", StringComparison.OrdinalIgnoreCase))
							{
								return RedirectToAction("Index", "User");
							}
							else
							{
								ModelState.AddModelError("", "User role is not recognized.");
								Logger.LogWarning($"User {user.EmailId} has an unrecognized role: {user.Role}");
							}
						}
						else
						{
							ModelState.AddModelError("", "Invalid password. Please try again.");
							Logger.LogWarning($"Invalid password attempt for user {model.EmailId}.");
						}
					}
					else
					{
						ModelState.AddModelError("", "No user found with this email.");
						Logger.LogWarning($"Login attempt with non-existing email: {model.EmailId}");
					}
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "An error occurred during login: " + ex.Message;
					Logger.LogError($"Error during login attempt for email {model.EmailId}: {ex.Message}");
				}
			}

			var errorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
			ViewBag.ErrorMessage = errorMessage ?? ViewBag.ErrorMessage;

			return View(model);
		}

		// Uncomment this method to enable the logout functionality
		// public ActionResult Logout()
		// {
		//     Session.Clear();
		//     Session.Abandon();
		//     return RedirectToAction("Login", "Login");
		// }
	}
}
