using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	[AdminAuthorize]
	public class AssignedTestController : Controller
	{
		AssignedTestDataAccess assignedtest = new AssignedTestDataAccess();

		public async Task<ActionResult> Index()
		{
			try
			{
				var assignedTests = await assignedtest.GetAllAssignedTestsAsync();
				var userEmails = await assignedtest.GetAllUserEmailsAsync();
				var testNames = await assignedtest.GetAllTestNamesAsync();

			
				// Logging
				//Logger.LogActivity("Admin accessed AssignedTest Index page.");

				ViewBag.UserEmails = userEmails;
				ViewBag.TestNames = testNames;

				return View(assignedTests);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred while fetching assigned tests: " + ex.Message);
				TempData["ErrorMessage"] = "An error occurred while fetching assigned tests.";
				return RedirectToAction("Index");
			}
		}

		// POST: Assign Test
		[HttpPost]
		public async Task<ActionResult> AssignTest(string emailAddress, string testName)
		{
			if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(testName))
			{
				TempData["ErrorMessage"] = "Email address and test name cannot be empty.";
				Logger.LogActivity("Failed to assign test: Email or Test name was empty.");
				return RedirectToAction("Index");
			}

			try
			{
				await assignedtest.AssignTestToUserAsync(emailAddress, testName);
				TempData["Message"] = "Test assigned successfully.";

				// Log successful assignment
				Logger.LogActivity($"Test '{testName}' assigned to user '{emailAddress}'.");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "An error occurred while assigning the test: " + ex.Message;

				// Log error
				Logger.LogError("Error occurred while assigning test: " + ex.Message);
			}

			return RedirectToAction("Index");
		}


		// POST: Remove Assigned Tests
		[HttpPost]
		public async Task<ActionResult> RemoveTests(string emailAddress, string testName)
		{
			if (string.IsNullOrEmpty(emailAddress) && string.IsNullOrEmpty(testName))
			{
				TempData["ErrorMessage"] = "At least one of Email address or Test name must be provided.";
				Logger.LogActivity("Failed to remove tests: Neither email nor test name was provided.");
				return RedirectToAction("Index");
			}

			try
			{
				await assignedtest.RemoveAssignedTest(emailAddress, testName);
				TempData["Message"] = "Assigned tests removed successfully.";

				// Log successful removal
				Logger.LogActivity($"Test '{testName}' removed from user '{emailAddress}'.");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "An error occurred while removing the tests: " + ex.Message;

				// Log error
				Logger.LogError("Error occurred while removing test: " + ex.Message);
			}

			return RedirectToAction("Index");
		}
	}
}
