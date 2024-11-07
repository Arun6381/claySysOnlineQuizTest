using ClaysysOnlineQuizTest.Models;
using ClaysysOnlineQuizTest.Utitlities; // Import Logger
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ClaysysOnlineQuizTest.DataAccessLayer
{
	public class QuestionDataAccess
	{
		internal string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ToString();

		public void AddQuestion(Question question, string TestName)
		{
			SqlConnection con = new SqlConnection(connectionString);
			try
			{
				SqlCommand cmd = new SqlCommand("AddQuestion", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@TestName", TestName);
				cmd.Parameters.AddWithValue("@QuestionText", question.QuestionText);
				cmd.Parameters.AddWithValue("@Option1", question.Option1);
				cmd.Parameters.AddWithValue("@Option2", question.Option2);
				cmd.Parameters.AddWithValue("@Option3", question.Option3);
				cmd.Parameters.AddWithValue("@Option4", question.Option4);
				cmd.Parameters.AddWithValue("@CorrectOption", question.CorrectOption);
				cmd.Parameters.AddWithValue("@Score", question.Score);

				con.Open();
				cmd.ExecuteNonQuery();
				Logger.LogActivity($"Question added successfully for test '{TestName}'");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error adding question for test '{TestName}': {ex.Message}");
				throw;
			}
			finally
			{
				con.Close();
			}
		}

		public void RemoveQuestion(int questionId)
		{
			SqlConnection con = new SqlConnection(connectionString);
			try
			{
				SqlCommand cmd = new SqlCommand("RemoveQuestion", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@QuestionID", questionId);

				con.Open();
				cmd.ExecuteNonQuery();
				Logger.LogActivity($"Question with ID {questionId} removed successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error removing question with ID {questionId}: {ex.Message}");
				throw;
			}
			finally
			{
				con.Close();
			}
		}

		public List<Question> GetAllQuestions()
		{
			List<Question> questions = new List<Question>();
			SqlConnection con = new SqlConnection(connectionString);
			try
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM Question", con);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					questions.Add(new Question
					{
						QuestionID = Convert.ToInt32(reader["QuestionID"]),
						TestName = reader["TestName"].ToString(),
						QuestionText = reader["QuestionText"].ToString(),
						Option1 = reader["Option1"].ToString(),
						Option2 = reader["Option2"].ToString(),
						Option3 = reader["Option3"].ToString(),
						Option4 = reader["Option4"].ToString(),
						CorrectOption = Convert.ToInt32(reader["CorrectOption"]),
						Score = Convert.ToInt32(reader["Score"]),
						CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
					});
				}
				Logger.LogActivity("Fetched all questions successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching all questions: {ex.Message}");
				throw;
			}
			finally
			{
				con.Close();
			}
			return questions;
		}

		public List<Question> GetQuestionsByTestName(string testName)
		{
			List<Question> questions = new List<Question>();
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("GetQuestionByTestName", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@TestName", testName);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Question question = new Question
							{
								QuestionID = (int)reader["QuestionID"],
								TestName = (string)reader["TestName"],
								QuestionText = (string)reader["QuestionText"],
								Option1 = (string)reader["Option1"],
								Option2 = (string)reader["Option2"],
								Option3 = (string)reader["Option3"],
								Option4 = (string)reader["Option4"],
								CorrectOption = (int)reader["CorrectOption"],
								Score = (int)reader["Score"],
								CreatedAt = (DateTime)reader["CreatedAt"]
							};
							questions.Add(question);
						}
					}
				}
				Logger.LogActivity($"Fetched questions for test '{testName}' successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error fetching questions for test '{testName}': {ex.Message}");
				throw;
			}
			finally
			{
				conn.Close();
			}
			return questions;
		}
	}
}
