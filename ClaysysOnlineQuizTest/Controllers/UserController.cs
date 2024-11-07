using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	public class UserController : Controller
	{
		private UserDataAccess userDataAccess = new UserDataAccess();

		[UserAuthorize]

		public ActionResult Index(AssignTest result)
		{
			var value = Session["EmailID"] as string;
			ViewBag.SessionValue = value;
			var valueid = Session["UserID"];
			ViewBag.SessionValueid = valueid;

			return View(result);
		}
		[UserAuthorize]
		public ActionResult AssignedQuestions(string emailAddress)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(emailAddress))
				{
					return new HttpStatusCodeResult(400, "Email address is required.");
				}

				var questions = userDataAccess.GetAssignedQuestionsByEmail(emailAddress);

				if (questions == null || !questions.Any())
				{
					return HttpNotFound("No questions found for the provided email address.");
				}

				return View(questions);
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching questions for {emailAddress}: {ex.Message}");
				ViewBag.ErrorMessage = "An error occurred while fetching assigned question. Please try again later.";
				return View("Error");
			}
		}
		[UserAuthorize]
		public ActionResult AllAssignedTest(string emailAddress)
		{
			var value = Session["EmailID"] as string;
			ViewBag.SessionValue = value;

			try
			{
				var assignedTests = userDataAccess.GetAssignedTestsByEmail(value);

				if (assignedTests == null || !assignedTests.Any())
				{
					ViewBag.NoAssignedTestsMessage = "No tests have been assigned to you. Please contact the admin for assistance.";
					return View(new List<AssignTest>()); // Return an empty list to the view
				}

				return View(assignedTests);
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching assigned tests for {value}: {ex.Message}");
				ViewBag.ErrorMessage = "An error occurred while fetching assigned tests. Please try again later.";
				return View("Error");
			}
		}
		[UserAuthorize]
		public ActionResult Learning()
		{
			return View();
		}
	}
}
