using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClaysysOnlineQuizTest.Utitlities;

namespace ClaysysOnlineQuizTest.Controllers
{
	[AdminAuthorize]
	public class AdminController : Controller
	{
		AdminDataAccess adminaccess = new AdminDataAccess();
		UserDataAccess useraccess = new UserDataAccess();

		[AdminAuthorize]
		public ActionResult Index()
		{
			return View();
		}

		[AdminAuthorize]
		public async Task<ActionResult> Create()
		{
			var userEmails = await adminaccess.GetAllUserEmailsAsync();

			ViewBag.UserEmails = userEmails;

			return View();
		}

		[HttpPost]
		[AdminAuthorize]
		public ActionResult Create(string email)
		{
			if (!string.IsNullOrEmpty(email))
			{
				try
				{
					adminaccess.AddAdmin(email);
					// Log successful admin creation
					Logger.LogActivity($"Admin with email {email} created successfully.");
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					// Log exception
					Logger.LogError($"Error adding admin with email {email}: {ex.Message}");
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			return View();
		}

		[AdminAuthorize]
		public ActionResult Delete()
		{
			return View();
		}

		[HttpPost]
		[AdminAuthorize]
		public ActionResult Delete(string email)
		{
			if (!string.IsNullOrEmpty(email))
			{
				try
				{
					adminaccess.RemoveAdminByEmail(email);
					// Log successful admin removal
					Logger.LogActivity($"Admin with email {email} removed successfully.");
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					// Log exception
					Logger.LogError($"Error removing admin with email {email}: {ex.Message}");
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			return View();
		}

		[AdminAuthorize]
		public ActionResult GetAllUser()
		{
			List<Users> users = useraccess.GetAllUsers();
			return View(users);
		}

		[AdminAuthorize]
		public ActionResult GetAllAdmins()
		{
			List<Admin> admins = adminaccess.GetAllAdmins();
			return View(admins);
		}

		[HttpGet]
		[AdminAuthorize]
		public ActionResult ConfirmDeleteUser(int id)
		{
			var user = useraccess.GetUserById(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteUser(int id)
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid user ID.";
				return RedirectToAction("GetAllUser");
			}

			try
			{
				useraccess.DeleteUser(id);
				// Log successful user deletion
				Logger.LogActivity($"User with ID {id} deleted successfully.");
				TempData["Message"] = "User deleted successfully.";
			}
			catch (Exception ex)
			{
				// Log exception
				Logger.LogError($"Error deleting user with ID {id}: {ex.Message}");
				TempData["ErrorMessage"] = "An error occurred while deleting the user: " + ex.Message;
			}

			return RedirectToAction("GetAllUser");
		}

		public ActionResult UserTestResult()
		{
			List<UserTestResult> results = adminaccess.GetAllTestResults();
			return View(results);
		}
	}
}
