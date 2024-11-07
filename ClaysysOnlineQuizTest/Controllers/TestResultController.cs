using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utilities; // Add this using statement
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	public class TestResultController : Controller
	{
		UserTestResultDataAccess userTestResultDataAccess = new UserTestResultDataAccess();

		[UserAuthorize]
		public ActionResult Results(int userId)
		{
			try
			{
				// Log the action of navigating to the results page
				Logger.LogActivity($"Navigating to Results page for UserID: {userId}");

				List<UserTestResult> results = userTestResultDataAccess.GetResultsByUserId(userId);

				// Log the retrieval of test results
				Logger.LogActivity($"Successfully retrieved {results.Count} results for UserID: {userId}");

				return View(results);
			}
			catch (Exception ex)
			{
				// Log the error in case of an exception
				Logger.LogError($"Error while retrieving results for UserID: {userId}. Exception: {ex.Message}");

				ModelState.AddModelError("", "An error occurred while retrieving results: " + ex.Message);
				return View(new List<UserTestResult>());
			}
		}

		[HttpPost]
		[UserAuthorize]
		public ActionResult InsertResult(List<Question> answers)
		{
			try
			{
				if (Session["UserID"] != null && Session["EmailID"] != null)
				{
					int userId = (int)Session["UserID"];
					string emailAddress = (string)Session["EmailID"];

					// Log attempt to insert test result
					Logger.LogActivity($"UserID: {userId} is submitting results for email: {emailAddress}");

					string testName = answers.FirstOrDefault()?.TestName;

					if (string.IsNullOrEmpty(testName))
					{
						Logger.LogWarning("Test name not provided when submitting results.");
						ModelState.AddModelError("", "Test name is not provided.");
						return View(answers);
					}

					int totalScore = ScoringUtility.CalculateTotalScore(answers);

					// Log the score calculation
					Logger.LogActivity($"Calculated total score: {totalScore} for Test: {testName} by UserID: {userId}");

					userTestResultDataAccess.InsertTestResult(userId, emailAddress, testName, totalScore);

					// Log successful insertion of test result
					Logger.LogActivity($"Test result successfully inserted for UserID: {userId}, Test: {testName}, Score: {totalScore}");

					return RedirectToAction("Results", new { userId });
				}

				// Log when session values are missing
				Logger.LogWarning("User session data (UserID or EmailID) is missing.");

				return RedirectToAction("Results", "TestResult");
			}
			catch (Exception ex)
			{
				// Log the error in case of an exception
				Logger.LogError($"Error while inserting test result. Exception: {ex.Message}");

				ModelState.AddModelError("", "An error occurred while inserting the result: " + ex.Message);
				return View(answers);
			}
		}
	}
}
