using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities; // Assuming Logger is in this namespace

namespace ClaysysOnlineQuizTest.Controllers
{
	public class TestController : Controller
	{
		private TestDataAccess testDataAccess = new TestDataAccess();

		[AdminAuthorize]
		public ActionResult Index()
		{
			try
			{
				// Log access to the Index page
				Logger.LogActivity("Accessed Test Index page.");

				List<Test> tests = testDataAccess.GetAllTests();
				return View(tests);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred while accessing the Test Index page: " + ex.Message);
				return RedirectToAction("Error");
			}
		}

		public ActionResult GetTestByTopicId(int topicId)
		{
			try
			{
				Logger.LogActivity($"Attempting to retrieve tests for TopicId: {topicId}");

				List<Test> tests = testDataAccess.GetTestsByTopicId(topicId);
				if (tests == null || tests.Count == 0)
				{
					Logger.LogActivity($"No tests found for TopicId: {topicId}");
					return RedirectToAction("NoTestsFound");
				}
				return View("Index", tests);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred while retrieving tests for TopicId: " + topicId + " - " + ex.Message);
				ModelState.AddModelError("", "An error occurred while retrieving tests: " + ex.Message);
				return RedirectToAction("Error");
			}
		}

		public ActionResult Create()
		{
			Logger.LogActivity("Accessed Create Test page.");
			return View();
		}

		[HttpPost]
		public ActionResult Create(string testName, string description, int createdByAdminID, string topicName, string testImage)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Log test creation attempt
					Logger.LogActivity($"Attempting to create a new test with name: {testName}");

					testDataAccess.CreateTest(testName, description, createdByAdminID, topicName, testImage);
					Logger.LogActivity($"Test created successfully: {testName}");

					return RedirectToAction("Index", "Topic");
				}
				else
				{
					Logger.LogWarning("Model validation failed during test creation.");
				}
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred while creating test: " + ex.Message);
				ModelState.AddModelError("", "An error occurred while creating the test: " + ex.Message);
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int testID)
		{
			try
			{
				// Log test deletion attempt
				Logger.LogActivity($"Attempting to delete test with ID: {testID}");

				testDataAccess.RemoveTest(testID);

				Logger.LogActivity($"Test with ID: {testID} deleted successfully.");
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error occurred while deleting test with ID: " + testID + " - " + ex.Message);
				ModelState.AddModelError("", "An error occurred while deleting the test: " + ex.Message);
				return RedirectToAction("Index");
			}
		}

		public ActionResult NoTestsFound()
		{
			Logger.LogActivity("No tests found for the specified topic.");
			return View();
		}

		public ActionResult Error()
		{
			Logger.LogActivity("Error page accessed.");
			return View();
		}
	}
}
