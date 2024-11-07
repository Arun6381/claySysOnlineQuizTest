using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class AssignedTestDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public async Task<List<AssignTest>> GetAllAssignedTestsAsync()
		{
			List<AssignTest> assignedTests = new List<AssignTest>();
			Logger.LogActivity("Starting GetAllAssignedTestsAsync.");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("GetAllAssignedTest", connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					try
					{
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								assignedTests.Add(new AssignTest
								{
									AssignedTestID = (int)reader["AssignedTestID"],
									UserID = (int)reader["UserID"],
									TestID = (int)reader["TestID"],
									TestName = reader["TestName"].ToString(),
									EmailAddress = reader["EmailAddress"].ToString(),
									AssignedAt = (DateTime)reader["AssignedAt"]
								});
							}
						}
						Logger.LogActivity("GetAllAssignedTestsAsync completed successfully.");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in GetAllAssignedTestsAsync: {ex.Message}");
						throw;
					}
					finally
					{
						command.Dispose();
					}
				}
			}

			return assignedTests;
		}

		public async Task AssignTestToUserAsync(string emailAddress, string testName)
		{
			Logger.LogActivity($"Starting AssignTestToUserAsync for Email: {emailAddress}, Test: {testName}");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("AssignTestToUserByTestNameAndEmail", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@EmailAddress", emailAddress);
					command.Parameters.AddWithValue("@TestName", testName);

					try
					{
						await command.ExecuteNonQueryAsync();
						Logger.LogActivity($"AssignTestToUserAsync completed for Email: {emailAddress}, Test: {testName}");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in AssignTestToUserAsync for Email: {emailAddress}, Test: {testName} - {ex.Message}");
						throw;
					}
					finally
					{
						command.Dispose();
					}
				}
			}
		}

		public async Task<List<string>> GetAllUserEmailsAsync()
		{
			List<string> emails = new List<string>();
			Logger.LogActivity("Starting GetAllUserEmailsAsync.");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("SELECT EmailAddress FROM Users WHERE Role = 'user'", connection))
				{
					try
					{
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								emails.Add(reader["EmailAddress"].ToString());
							}
						}
						Logger.LogActivity("GetAllUserEmailsAsync completed successfully.");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in GetAllUserEmailsAsync: {ex.Message}");
						throw;
					}
				}
			}

			return emails;
		}

		public async Task<List<string>> GetAllTestNamesAsync()
		{
			List<string> testNames = new List<string>();
			Logger.LogActivity("Starting GetAllTestNamesAsync.");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("SELECT TestName FROM Tests", connection))
				{
					try
					{
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								testNames.Add(reader["TestName"].ToString());
							}
						}
						Logger.LogActivity("GetAllTestNamesAsync completed successfully.");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in GetAllTestNamesAsync: {ex.Message}");
						throw;
					}
				}
			}

			return testNames;
		}

		public async Task RemoveAssignedTest(string emailAddress, string testName)
		{
			Logger.LogActivity($"Starting RemoveAssignedTest for Email: {emailAddress}, Test: {testName}");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("RemoveAssignedTestbyTestNameandEmailid", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@TestName", testName);
					command.Parameters.AddWithValue("@EmailAddress", emailAddress);

					try
					{
						await command.ExecuteNonQueryAsync();
						Logger.LogActivity($"RemoveAssignedTest completed for Email: {emailAddress}, Test: {testName}");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in RemoveAssignedTest for Email: {emailAddress}, Test: {testName} - {ex.Message}");
						throw;
					}
				}
			}
		}

		public async Task RemoveAllAssignedTestsByEmailAsync(string emailAddress)
		{
			Logger.LogActivity($"Starting RemoveAllAssignedTestsByEmailAsync for Email: {emailAddress}");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("RemoveAllAssignedTestsByEmail", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@EmailAddress", emailAddress);

					try
					{
						await command.ExecuteNonQueryAsync();
						Logger.LogActivity($"RemoveAllAssignedTestsByEmailAsync completed for Email: {emailAddress}");
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error in RemoveAllAssignedTestsByEmailAsync for Email: {emailAddress} - {ex.Message}");
						throw;
					}
					finally
					{
						command.Dispose();
					}
				}
			}
		}
	}
}
