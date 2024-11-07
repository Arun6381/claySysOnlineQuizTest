using ClaysysOnlineQuizTest.DataAccessLayer;
using ClaysysOnlineQuizTest.Filters;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utilities; // Add the Logger using statement
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Controllers
{
	[AdminAuthorize]
	public class TopicController : Controller
	{
		private readonly TopicDataAccess topicDataAccess = new TopicDataAccess();

		// GET: Topic/Index
		public ActionResult Index()
		{
			try
			{
				// Log activity when the Index page is accessed
				Logger.LogActivity("Accessing the list of all topics.");

				var topics = topicDataAccess.GetAllTopics();
				return View(topics);
			}
			catch (Exception ex)
			{
				// Log error if something goes wrong
				Logger.LogError($"Error while fetching all topics: {ex.Message}");
				ModelState.AddModelError("", "An error occurred while fetching topics.");
				return View();
			}
		}

		// GET: Topic/Add
		public ActionResult Add()
		{
			return View();
		}

		// POST: Topic/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(string topicName, string imageBase64)
		{
			try
			{
				if (string.IsNullOrEmpty(topicName))
				{
					Logger.LogWarning("Attempted to add a topic without providing a name.");
					ModelState.AddModelError("", "Topic name is required.");
					return View();
				}

				// Log the addition of a new topic
				Logger.LogActivity($"Adding new topic: {topicName}");

				topicDataAccess.AddNewTopic(topicName, imageBase64); // Pass Base64 image string

				// Log successful addition
				Logger.LogActivity($"New topic '{topicName}' added successfully.");
				TempData["Message"] = "New topic added successfully.";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error while adding topic: {ex.Message}");
				ModelState.AddModelError("", $"An error occurred: {ex.Message}");
				return View();
			}
		}

		// GET: Topic/Delete
		public ActionResult Delete()
		{
			return View();
		}

		// POST: Topic/Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string topicName)
		{
			try
			{
				if (string.IsNullOrEmpty(topicName))
				{
					Logger.LogWarning("Attempted to delete a topic without providing a name.");
					ModelState.AddModelError("", "Topic name is required.");
					return View();
				}

				// Log the deletion attempt
				Logger.LogActivity($"Attempting to delete topic: {topicName}");

				topicDataAccess.RemoveTopicByName(topicName);

				// Log successful deletion
				Logger.LogActivity($"Topic '{topicName}' deleted successfully.");
				TempData["Message"] = "Topic removed successfully.";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error while deleting topic: {ex.Message}");
				ModelState.AddModelError("", $"An error occurred: {ex.Message}");
				return View();
			}
		}
	}
}
