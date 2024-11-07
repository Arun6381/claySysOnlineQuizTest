using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Models
{
	public class AssignTest
	{
		public int AssignedTestID { get; set; }
		public int UserID { get; set; }
		public int TestID { get; set; }
		public string TestName { get; set; }
		public string EmailAddress { get; set; }
		public string Description { get; set; }
		public DateTime AssignedAt { get; set; }
		public string TestImage { get; set; }
	}
}