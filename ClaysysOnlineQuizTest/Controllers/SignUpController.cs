using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities; // Assuming Logger is in this namespace
using System;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	public class SignUpController : Controller
	{
		private SignUpDataAccess createuser = new SignUpDataAccess();
		private SelectStateAndCities drop = new SelectStateAndCities();

		public ActionResult Create()
		{
			var user = new SignUp();
			drop.PopulateStatesAndCities(user);
			Logger.LogActivity("Navigating to Create SignUp page.");
			return View(user);
		}

		[HttpPost]
		public ActionResult Create(SignUp user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Log user creation attempt
					Logger.LogActivity($"Attempting to create user with email: {user.EmailId}");

					createuser.InsertUser(user);

					// Log success
					Logger.LogActivity($"User with email: {user.EmailId} created successfully.");

					return RedirectToAction("Login", "Login");
				}
				else
				{
					Logger.LogWarning("Model validation failed during user sign up.");
				}

				drop.PopulateStatesAndCities(user);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError($"Error during user sign up: {ex.Message}");

				ModelState.AddModelError("", "An error occurred while saving the data. Please try again later. Error details: " + ex.Message);
				drop.PopulateStatesAndCities(user);
			}

			return View(user);
		}

		[HttpPost]
		public JsonResult CheckEmail(string email)
		{
			try
			{
				Logger.LogActivity($"Checking if email {email} is already taken.");

				bool isTaken = createuser.CheckEmailExists(email);

				// Log the result of the check
				Logger.LogActivity($"Email {email} check result: {isTaken}");

				return Json(new { isTaken });
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError($"Error checking email {email}: {ex.Message}");
				return Json(new { isTaken = false });
			}
		}
	}
}
