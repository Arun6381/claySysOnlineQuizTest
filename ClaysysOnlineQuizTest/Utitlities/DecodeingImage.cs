using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaysysOnlineQuizTest.Utitlities
{
	public class DecodeingImage
	{
		public string DecodeBase64Image(string base64Image)
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