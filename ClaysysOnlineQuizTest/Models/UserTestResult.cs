using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Models
{
	public class UserTestResult
	{
		public int TestResultID { get; set; }
		public string TestName { get; set; }
		public int UserID { get; set; }
		public string EmailAddress { get; set; }
		public int TestID { get; set; }
		public int TotalScore { get; set; }
		public DateTime CompletedAt { get; set; }
	}
}