using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities;  // Import the Logger class

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class SignUpDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public bool CheckEmailExists(string email)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					Logger.LogActivity($"Checking if email exists: {email}");
					SqlCommand command = new SqlCommand("CheckEmailExists", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@Email", email);

					connection.Open();
					bool emailExists = (int)command.ExecuteScalar() > 0;
					Logger.LogActivity($"Email check completed: {email} - Exists: {emailExists}");
					return emailExists;
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error checking email existence for {email}: {ex.Message}");
					return false;
				}
			}
		}

		public SignUp GetByEmail(string email)
		{
			SignUp user = null;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					Logger.LogActivity($"Retrieving user by email: {email}");
					SqlCommand command = new SqlCommand("GetUserByEmail", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@Email", email);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							user = new SignUp
							{
								UserID = (int)reader["UserID"],
								FirstName = reader["FirstName"].ToString(),
								LastName = reader["LastName"].ToString(),
								Gender = reader["Gender"].ToString(),
								PhoneNumber = reader["PhoneNumber"].ToString(),
								EmailId = reader["EmailAddress"].ToString(),
								Address = reader["Address"].ToString(),
								UserState = reader["State"].ToString(),
								UserCity = reader["City"].ToString(),
								UserName = reader["Username"].ToString(),
								EducationQualification = reader["Education"].ToString(),
								UserPassword = reader["PasswordHash"].ToString(),
								Role = reader["Role"].ToString()
							};
						}
					}
					Logger.LogActivity($"User retrieval by email completed: {email}");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error retrieving user by email {email}: {ex.Message}");
				}
			}

			return user;
		}

		public void InsertUser(SignUp user)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					Logger.LogActivity($"Inserting new user: {user.EmailId}");
					SqlCommand command = new SqlCommand("InsertUser", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@FirstName", user.FirstName);
					command.Parameters.AddWithValue("@LastName", user.LastName);
					command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
					command.Parameters.AddWithValue("@Gender", user.Gender);
					command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
					command.Parameters.AddWithValue("@Email", user.EmailId);
					command.Parameters.AddWithValue("@Address", user.Address);
					command.Parameters.AddWithValue("@State", user.UserState);
					command.Parameters.AddWithValue("@City", user.UserCity);
					command.Parameters.AddWithValue("@Username", user.UserName);
					command.Parameters.AddWithValue("@Education", user.EducationQualification);

					string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
					command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

					connection.Open();
					command.ExecuteNonQuery();
					Logger.LogActivity($"User inserted successfully: {user.EmailId}");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error inserting user {user.EmailId}: {ex.Message}");
				}
			}
		}
	}
}
