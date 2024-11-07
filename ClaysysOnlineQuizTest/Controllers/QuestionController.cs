using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClaysysOnlineQuizTest.Utitlities; // Assuming Logger is in this namespace

namespace ClaysysOnlineQuizTest.Controllers
{
	[AdminAuthorize]
	public class QuestionController : Controller
	{
		private readonly QuestionDataAccess questionAccess = new QuestionDataAccess();

		[AdminAuthorize]
		public ActionResult Index()
		{
			try
			{
				// Log activity
				Logger.LogActivity("Fetching all questions.");

				List<Question> questions = questionAccess.GetAllQuestions();

				// Log success
				Logger.LogActivity("Successfully fetched all questions.");

				return View(questions);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error fetching all questions: " + ex.Message);

				TempData["Message"] = "An error occurred while fetching questions.";
				return RedirectToAction("Index");
			}
		}

		[AdminAuthorize]
		public ActionResult Add()
		{
			Logger.LogActivity("Navigating to Add Question page.");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AdminAuthorize]
		public ActionResult Add(Question question, string TestName)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Log activity
					Logger.LogActivity($"Attempting to add question '{question.QuestionText}' for Test '{TestName}'.");

					questionAccess.AddQuestion(question, TestName);

					// Log success
					Logger.LogActivity($"Question '{question.QuestionText}' added successfully under Test '{TestName}'.");

					TempData["Message"] = "Question added successfully.";
					return RedirectToAction("Add", "Question");
				}
				catch (Exception ex)
				{
					// Log error
					Logger.LogError("Error adding question: " + ex.Message);

					ModelState.AddModelError("", "An error occurred: " + ex.Message);
				}
			}

			return View(question);
		}

		[AdminAuthorize]
		public ActionResult Delete(int id)
		{
			try
			{
				Logger.LogActivity($"Attempting to fetch question with ID {id} for deletion.");

				Question question = questionAccess.GetAllQuestions().Find(q => q.QuestionID == id);

				if (question == null)
				{
					Logger.LogWarning($"Question with ID {id} not found.");
					return HttpNotFound();
				}

				Logger.LogActivity($"Question with ID {id} fetched successfully.");
				return View(question);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError("Error fetching question for deletion: " + ex.Message);

				TempData["Message"] = "An error occurred while fetching the question.";
				return RedirectToAction("Index");
			}
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[AdminAuthorize]
		public ActionResult DeleteConfirmed(int id)
		{
			try
			{
				// Log activity
				Logger.LogActivity($"Attempting to delete question with ID {id}.");

				questionAccess.RemoveQuestion(id);

				// Log success
				Logger.LogActivity($"Question with ID {id} removed successfully.");

				TempData["Message"] = "Question removed successfully.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError($"Error removing question with ID {id}: " + ex.Message);

				TempData["Message"] = "An error occurred while deleting the question.";
				return RedirectToAction("Index");
			}
		}

		[AdminAuthorize]
		public ActionResult GetQuestionsByTest(string testName)
		{
			try
			{
				Logger.LogActivity($"Fetching questions for Test: {testName}.");

				List<Question> questions = questionAccess.GetQuestionsByTestName(testName);

				// Log success
				Logger.LogActivity($"Successfully fetched questions for Test: {testName}");

				return View(questions);
			}
			catch (Exception ex)
			{
				// Log error
				Logger.LogError($"Error fetching questions for Test: {testName}: " + ex.Message);

				TempData["Message"] = "An error occurred while fetching questions.";
				return RedirectToAction("Index");
			}
		}
	}
}
