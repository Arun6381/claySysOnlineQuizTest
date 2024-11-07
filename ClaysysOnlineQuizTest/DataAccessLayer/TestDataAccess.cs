using ClaysysOnlineQuizTest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ClaysysOnlineQuizTest.Utitlities;  // Import the Logger class

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class TestDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public List<Test> GetAllTests()
		{
			var tests = new List<Test>();
			SqlConnection conn = null;

			try
			{
				Logger.LogActivity("Fetching all tests...");
				conn = new SqlConnection(connectionString);
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("GetAllTest", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							tests.Add(new Test
							{
								TestID = (int)reader["TestID"],
								TestName = (string)reader["TestName"],
								TestImage = DecodeBase64Image(reader["TestImage"].ToString()),
								Description = (string)reader["Description"],
								CreatedAt = (DateTime)reader["CreatedAt"],
								CreatedByAdminID = (int)reader["CreatedByAdminID"],
								TopicID = (int)reader["TopicID"]
							});
						}
					}
				}
				Logger.LogActivity("Fetched all tests successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching all tests: {ex.Message}");
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
					conn.Close();
			}

			return tests;
		}

		private string DecodeBase64Image(string base64Image)
		{
			if (string.IsNullOrEmpty(base64Image))
			{
				return null;
			}

			string imageData = base64Image;
			if (base64Image.Contains(","))
			{
				imageData = base64Image.Split(',')[1];
			}

			byte[] imageBytes = Convert.FromBase64String(imageData);
			string imageBase64 = Convert.ToBase64String(imageBytes);

			return $"data:image/png;base64,{imageBase64}";
		}

		public Test GetTestById(int testID)
		{
			Test test = null;
			SqlConnection conn = null;

			try
			{
				Logger.LogActivity($"Fetching test by ID: {testID}...");
				conn = new SqlConnection(connectionString);
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("GetTestById", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@TestID", testID);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							test = new Test
							{
								TestID = (int)reader["TestID"],
								TestName = (string)reader["TestName"],
								TestImage = DecodeBase64Image(reader["TestImage"].ToString()),
								Description = (string)reader["Description"],
								CreatedAt = (DateTime)reader["CreatedAt"],
								CreatedByAdminID = (int)reader["CreatedByAdminID"],
								TopicID = (int)reader["TopicID"]
							};
						}
					}
				}
				Logger.LogActivity($"Fetched test by ID: {testID} successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching test by ID {testID}: {ex.Message}");
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
					conn.Close();
			}

			return test;
		}

		public List<Test> GetTestsByTopicId(int topicId)
		{
			var tests = new List<Test>();
			SqlConnection conn = null;

			try
			{
				Logger.LogActivity($"Fetching tests by topic ID: {topicId}...");
				conn = new SqlConnection(connectionString);
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("GetTestsByTopicId", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@TopicID", topicId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							tests.Add(new Test
							{
								TestID = (int)reader["TestID"],
								TestName = (string)reader["TestName"],
								TestImage = DecodeBase64Image(reader["TestImage"].ToString()),
								Description = (string)reader["Description"],
								CreatedAt = (DateTime)reader["CreatedAt"],
								CreatedByAdminID = (int)reader["CreatedByAdminID"],
								TopicID = (int)reader["TopicID"]
							});
						}
					}
				}
				Logger.LogActivity($"Fetched tests by topic ID: {topicId} successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching tests by topic ID {topicId}: {ex.Message}");
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
					conn.Close();
			}

			return tests;
		}

		public void CreateTest(string testName, string description, int createdByAdminID, string topicName, string testImage)
		{
			SqlConnection conn = null;

			try
			{
				Logger.LogActivity("Creating new test...");
				conn = new SqlConnection(connectionString);
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("CreateTestForTopic", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@TestName", testName);
					cmd.Parameters.AddWithValue("@Description", description);
					cmd.Parameters.AddWithValue("@CreatedByAdminID", createdByAdminID);
					cmd.Parameters.AddWithValue("@TopicName", topicName);
					cmd.Parameters.AddWithValue("@TestImage", testImage);

					cmd.ExecuteNonQuery();
				}

				Logger.LogActivity($"Created new test successfully: {testName} for topic {topicName}.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error creating test {testName}: {ex.Message}");
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
					conn.Close();
			}
		}

		public void RemoveTest(int testID)
		{
			SqlConnection conn = null;

			try
			{
				Logger.LogActivity($"Removing test with ID: {testID}...");
				conn = new SqlConnection(connectionString);
				conn.Open();

				using (SqlCommand cmd = new SqlCommand("RemoveTest", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@TestID", testID);

					cmd.ExecuteNonQuery();
				}

				Logger.LogActivity($"Test with ID: {testID} removed successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error removing test with ID {testID}: {ex.Message}");
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
					conn.Close();
			}
		}
	}
}
