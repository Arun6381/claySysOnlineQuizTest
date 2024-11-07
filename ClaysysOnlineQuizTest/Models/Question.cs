using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Models
{
	public class Question
	{
		public int QuestionID { get; set; }
		public string TestName { get; set; }
		[AllowHtml]
		public string QuestionText { get; set; }
		[AllowHtml]
		public string Option1 { get; set; }
		[AllowHtml]
		public string Option2 { get; set; }
		[AllowHtml]
		public string Option3 { get; set; }
		[AllowHtml]
		public string Option4 { get; set; }
		public int CorrectOption { get; set; }
		public int Score { get; set; }
		public DateTime CreatedAt { get; set; }
		public int UserAnswer { get; set; }
	}
}