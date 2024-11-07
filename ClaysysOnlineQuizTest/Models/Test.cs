using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Models
{
	public class Test
	{
		 public int TestID { get; set; }
        public string TestName { get; set; }
		public string TestImage { get; set; }
		public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByAdminID { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
	}
}