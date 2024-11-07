using ClaysysOnlineQuizTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ClaysysOnlineQuizTest.Utitlities;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class TopicDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		// Log the start and end of database operations, and capture errors
		public void AddNewTopic(string topicName, string imageBase64)
		{
			imageBase64 = imageBase64?.Trim();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				Logger.LogActivity($"Attempting to add new topic: {topicName}");

				using (SqlCommand command = new SqlCommand("AddNewTopic", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@TopicName", topicName);
					command.Parameters.AddWithValue("@ImageBase64", imageBase64);

					connection.Open();
					command.ExecuteNonQuery();
				}

				Logger.LogActivity($"Successfully added topic: {topicName}");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error while adding topic {topicName}: {ex.Message}");
				throw; // Re-throw the exception after logging
			}
			finally
			{
				connection.Close();
				Logger.LogActivity($"Connection closed for adding topic: {topicName}");
			}
		}

		public void RemoveTopicByName(string topicName)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				Logger.LogActivity($"Attempting to remove topic: {topicName}");

				using (SqlCommand command = new SqlCommand("RemoveTopicByName", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@TopicName", topicName);

					connection.Open();
					command.ExecuteNonQuery();
				}

				Logger.LogActivity($"Successfully removed topic: {topicName}");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error while removing topic {topicName}: {ex.Message}");
				throw; // Re-throw the exception after logging
			}
			finally
			{
				connection.Close();
				Logger.LogActivity($"Connection closed for removing topic: {topicName}");
			}
		}

		public List<Topic> GetAllTopics()
		{
			var topics = new List<Topic>();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				Logger.LogActivity("Fetching all topics from the database.");

				using (SqlCommand command = new SqlCommand("SELECT * FROM Topics", connection))
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							topics.Add(new Topic
							{
								TopicID = (int)reader["TopicID"],
								TopicName = reader["TopicName"].ToString(),
								ImageBase64 = DecodeBase64Image(reader["ImageBase64"].ToString()),
								CreatedAt = (DateTime)reader["CreatedAt"]
							});
						}
					}
				}

				Logger.LogActivity("Successfully fetched all topics.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error while fetching topics: {ex.Message}");
				throw; // Re-throw the exception after logging
			}
			finally
			{
				connection.Close();
				Logger.LogActivity("Connection closed after fetching topics.");
			}
			return topics;
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
	}
}
