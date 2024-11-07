
using ClaysysOnlineQuizTest.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Utitlities
{
	public class SelectStateAndCities
	{
		public void PopulateStatesAndCities(SignUp model)
		{
			model.State = new List<SelectListItem>
			{
				new SelectListItem { Value = "Tamil Nadu", Text = "Tamil Nadu" },
				new SelectListItem { Value = "Kerala", Text = "Kerala" },
				new SelectListItem { Value = "Karnataka", Text = "Karnataka" },
				new SelectListItem { Value = "Maharashtra", Text = "Maharashtra" },
				new SelectListItem { Value = "Goa", Text = "Goa" }
			};

			model.City = new List<SelectListItem>
			{
				new SelectListItem { Value = "Coimbatore", Text = "Coimbatore" },
				new SelectListItem { Value = "Chennai", Text = "Chennai" },
				new SelectListItem { Value = "Madurai", Text = "Madurai" },
				new SelectListItem { Value = "Palakkad", Text = "Palakkad" },
				new SelectListItem { Value = "Kochi", Text = "Kochi" },
				new SelectListItem { Value = "Thiruvananthapuram", Text = "Thiruvananthapuram" },
				new SelectListItem { Value = "Bengaluru", Text = "Bengaluru" },
				new SelectListItem { Value = "Mysuru", Text = "Mysuru" },
				new SelectListItem { Value = "Hubli", Text = "Hubli" },
				new SelectListItem { Value = "Mumbai", Text = "Mumbai" },
				new SelectListItem { Value = "Pune", Text = "Pune" },
				new SelectListItem { Value = "Nagpur", Text = "Nagpur" },
				new SelectListItem { Value = "Panaji", Text = "Panaji" },
				new SelectListItem { Value = "Margao", Text = "Margao" },
				new SelectListItem { Value = "Vasco da Gama", Text = "Vasco da Gama" }
			};
		}
	}
}
