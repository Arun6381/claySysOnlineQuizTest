using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class UserTestResultDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public void InsertTestResult(int userId, string emailAddress, string testName, int totalScore)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				Logger.LogActivity($"Opening connection to insert test result for UserID: {userId}, Email: {emailAddress}, Test: {testName}");

				using (SqlCommand command = new SqlCommand("InsertTestResult", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userId);
					command.Parameters.AddWithValue("@EmailAddress", emailAddress);
					command.Parameters.AddWithValue("@TestName", testName);
					command.Parameters.AddWithValue("@TotalScore", totalScore);

					connection.Open();
					Logger.LogActivity($"Executing stored procedure InsertTestResult for UserID: {userId}, Test: {testName}");

					command.ExecuteNonQuery();
					Logger.LogActivity($"Test result for UserID: {userId} inserted successfully.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error inserting test result for UserID: {userId}, Test: {testName}. Error: {ex.Message}");
				throw;
			}
			finally
			{
				connection.Close();
				Logger.LogActivity($"Connection closed for UserID: {userId}, Test: {testName}");
			}
		}

		public List<UserTestResult> GetResultsByUserId(int userId)
		{
			List<UserTestResult> results = new List<UserTestResult>();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				Logger.LogActivity($"Opening connection to fetch results for UserID: {userId}");

				using (SqlCommand command = new SqlCommand("GetResultsByUserId", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@UserID", userId);

					connection.Open();
					Logger.LogActivity($"Executing stored procedure GetResultsByUserId for UserID: {userId}");

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							results.Add(new UserTestResult
							{
								TestResultID = (int)reader["TestResultID"],
								UserID = userId,
								EmailAddress = reader["EmailAddress"].ToString(),
								TestID = (int)reader["TestID"],
								TestName = reader["TestName"].ToString(),
								TotalScore = (int)reader["TotalScore"],
								CompletedAt = (DateTime)reader["CompletedAt"]
							});
						}
					}
					Logger.LogActivity($"Successfully fetched results for UserID: {userId}");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching results for UserID: {userId}. Error: {ex.Message}");
				throw;
			}
			finally
			{
				connection.Close();
				Logger.LogActivity($"Connection closed for UserID: {userId}");
			}
			return results;
		}
	}
}
