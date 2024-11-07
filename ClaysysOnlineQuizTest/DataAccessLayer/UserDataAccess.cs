using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class UserDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public List<Users> GetAllUsers()
		{
			List<Users> users = new List<Users>();

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					Logger.LogActivity("Opening connection to fetch all users.");

					using (SqlCommand cmd = new SqlCommand("GetAllUsers", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Users user = new Users
								{
									UserID = (int)reader["UserID"],
									UserName = reader["UserName"].ToString(),
									EmailAddress = reader["EmailAddress"].ToString(),
									CreatedAt = (DateTime)reader["CreatedAt"]
								};
								users.Add(user);
							}
						}
					}
					Logger.LogActivity("Successfully fetched all users.");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error fetching users: {ex.Message}");
					throw;
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
						Logger.LogActivity("Connection closed.");
					}
				}
			}

			return users;
		}

		public void DeleteUser(int userId)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					Logger.LogActivity($"Opening connection to delete user with ID: {userId}");

					using (SqlCommand cmd = new SqlCommand("DeleteUser", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add(new SqlParameter("@UserID", userId));

						cmd.ExecuteNonQuery();
					}
					Logger.LogActivity($"User with ID {userId} deleted successfully.");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error deleting user with ID {userId}: {ex.Message}");
					throw;
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
						Logger.LogActivity("Connection closed.");
					}
				}
			}
		}

		public Users GetUserById(int userId)
		{
			Users user = null;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					Logger.LogActivity($"Opening connection to fetch user with ID: {userId}");

					using (SqlCommand cmd = new SqlCommand("GetUserById", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add(new SqlParameter("@UserId", userId));

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								user = new Users
								{
									UserID = (int)reader["UserID"],
									UserName = reader["UserName"].ToString(),
									EmailAddress = reader["EmailAddress"].ToString(),
									CreatedAt = (DateTime)reader["CreatedAt"]
								};
							}
						}
					}
					Logger.LogActivity($"Successfully fetched user with ID {userId}.");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error fetching user with ID {userId}: {ex.Message}");
					throw;
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
						Logger.LogActivity("Connection closed.");
					}
				}
			}

			return user;
		}

		public List<Question> GetAssignedQuestionsByEmail(string emailAddress)
		{
			List<Question> questions = new List<Question>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					Logger.LogActivity($"Opening connection to fetch assigned questions for email: {emailAddress}");

					using (SqlCommand command = new SqlCommand("GetAssignedQuestionsByEmail", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100) { Value = emailAddress });

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									Question question = new Question
									{
										QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID")),
										TestName = reader.GetString(reader.GetOrdinal("TestName")),
										QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
										Option1 = reader.GetString(reader.GetOrdinal("Option1")),
										Option2 = reader.GetString(reader.GetOrdinal("Option2")),
										Option3 = reader.GetString(reader.GetOrdinal("Option3")),
										Option4 = reader.GetString(reader.GetOrdinal("Option4")),
										CorrectOption = reader.GetInt32(reader.GetOrdinal("CorrectOption")),
										Score = reader.GetInt32(reader.GetOrdinal("Score")),
									};
									questions.Add(question);
								}
							}
							else
							{
								throw new Exception("No test is assigned to the user with this email address.");
							}
						}
					}
					Logger.LogActivity($"Successfully fetched assigned questions for email: {emailAddress}");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error fetching assigned questions for email {emailAddress}: {ex.Message}");
					throw;
				}
				finally
				{
					if (connection.State == ConnectionState.Open)
					{
						connection.Close();
						Logger.LogActivity("Connection closed.");
					}
				}
			}
			return questions;
		}

		public List<AssignTest> GetAssignedTestsByEmail(string emailAddress)
		{
			var assignedTests = new List<AssignTest>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					Logger.LogActivity($"Opening connection to fetch assigned tests for email: {emailAddress}");

					using (SqlCommand command = new SqlCommand("GetAssignedTestsByEmail", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@EmailAddress", emailAddress);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var assignedTest = new AssignTest
								{
									TestName = reader["TestName"].ToString(),
									AssignedAt = (DateTime)reader["AssignedAt"],
									Description = reader["Description"].ToString(),
									TestImage = DecodeBase64Image(reader["TestImage"].ToString()) // Decode Base64 image
								};
								assignedTests.Add(assignedTest);
							}
						}
					}
					Logger.LogActivity($"Successfully fetched assigned tests for email: {emailAddress}");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error fetching assigned tests for email {emailAddress}: {ex.Message}");
					throw;
				}
				finally
				{
					if (connection.State == ConnectionState.Open)
					{
						connection.Close();
						Logger.LogActivity("Connection closed.");
					}
				}
			}
			return assignedTests;
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
