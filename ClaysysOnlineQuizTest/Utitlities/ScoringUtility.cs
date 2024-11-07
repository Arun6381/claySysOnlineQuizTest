using ClaysysOnlineQuizTest.Models;
using System.Collections.Generic;

namespace ClaysysOnlineQuizTest.Utilities
{
	public static class ScoringUtility
	{
		public static int CalculateTotalScore(List<Question> answers)
		{
			int score = 0;
			foreach (var answer in answers)
			{
				if (answer.UserAnswer == answer.CorrectOption) // Assuming UserAnswer is collected
				{
					score++;
				}
			}
			return score;
		}
	}
}
