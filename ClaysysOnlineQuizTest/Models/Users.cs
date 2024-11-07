using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Models
{
	public class Users
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string EmailAddress { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}