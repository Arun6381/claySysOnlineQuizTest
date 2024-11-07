using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ClaysysOnlineQuizTest.Models;
using System.Threading.Tasks;
using ClaysysOnlineQuizTest.Utitlities;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class AdminDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public void AddAdmin(string email)
		{
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				conn.Open();
				Logger.LogActivity("Opened connection for AddAdmin.");

				using (SqlCommand cmd = new SqlCommand("AddAdminByEmail", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.ExecuteNonQuery();
					Logger.LogActivity($"Executed AddAdminByEmail stored procedure for email: {email}.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error in AddAdmin: {ex.Message}");
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
					Logger.LogActivity("Closed connection for AddAdmin.");
				}
			}
		}
		
		public async Task<List<string>> GetAllUserEmailsAsync()
		{
			var emails = new List<string>();
			using (var connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				Logger.LogActivity("Opened connection for GetAllUserEmailsAsync.");

				using (var command = new SqlCommand("SELECT EmailAddress FROM Users WHERE Role = 'user'", connection))
				{
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							emails.Add(reader["EmailAddress"].ToString());
						}
					}
				}

				Logger.LogActivity("Fetched all user emails in GetAllUserEmailsAsync.");
			}
			return emails;
		}

		public void RemoveAdminByEmail(string email)
		{
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				conn.Open();
				Logger.LogActivity("Opened connection for RemoveAdminByEmail.");

				using (SqlCommand cmd = new SqlCommand("RemoveAdminByEmail", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.ExecuteNonQuery();
					Logger.LogActivity($"Executed RemoveAdminByEmail stored procedure for email: {email}.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error in RemoveAdminByEmail: {ex.Message}");
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
					Logger.LogActivity("Closed connection for RemoveAdminByEmail.");
				}
			}
		}

		public List<Admin> GetAllAdmins()
		{
			List<Admin> admins = new List<Admin>();
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				conn.Open();
				Logger.LogActivity("Opened connection for GetAllAdmins.");

				using (SqlCommand cmd = new SqlCommand("GetAllAdmins", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Admin admin = new Admin
							{
								AdminID = reader.GetInt32(reader.GetOrdinal("AdminID")),
								UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
								UserName = reader.GetString(reader.GetOrdinal("UserName")),
								EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
								CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
							};
							admins.Add(admin);
						}
					}
					Logger.LogActivity("Fetched all admins in GetAllAdmins.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error in GetAllAdmins: {ex.Message}");
				throw;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
					Logger.LogActivity("Closed connection for GetAllAdmins.");
				}
			}
			return admins;
		}

		public List<UserTestResult> GetAllTestResults()
		{
			List<UserTestResult> testResults = new List<UserTestResult>();
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				Logger.LogActivity("Opened connection for GetAllTestResults.");

				using (SqlCommand command = new SqlCommand("GetAllTestResults", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							UserTestResult result = new UserTestResult
							{
								TestResultID = reader.GetInt32(reader.GetOrdinal("TestResultID")),
								UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
								EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
								TestID = reader.GetInt32(reader.GetOrdinal("TestID")),
								TotalScore = reader.GetInt32(reader.GetOrdinal("TotalScore")),
								CompletedAt = reader.GetDateTime(reader.GetOrdinal("CompletedAt"))
							};
							testResults.Add(result);
						}
					}
					Logger.LogActivity("Fetched all test results in GetAllTestResults.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error in GetAllTestResults: {ex.Message}");
				throw;
			}
			finally
			{
				if (connection.State == ConnectionState.Open)
				{
					connection.Close();
					Logger.LogActivity("Closed connection for GetAllTestResults.");
				}
			}
			return testResults;
		}
	}
}
