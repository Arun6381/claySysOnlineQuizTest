using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Models
{
	public class Topic
	{
		public int TopicID { get; set; }
		public string TopicName { get; set; }
		public string ImageBase64 { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}