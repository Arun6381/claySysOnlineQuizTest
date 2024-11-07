using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Models
{
	public class SignUp
	{
		[DisplayName("User ID")]
		public int UserID { get; set; }

		[DisplayName("Role")]
		public string Role { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[DisplayName("Date of Birth")]
		public string DateOfBirth { get; set; }

		[DisplayName("Gender")]
		public string Gender { get; set; }

		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

		[DisplayName("Email Address")]
		public string EmailId { get; set; }

		[DisplayName("Address")]
		public string Address { get; set; }

		[DisplayName("State")]
		public string UserState { get; set; }

		public List<SelectListItem> State { get; set; }

		[DisplayName("City")]
		public string UserCity { get; set; }

		public List<SelectListItem> City { get; set; }

		[DisplayName("Username")]
		public string UserName { get; set; }

		[DisplayName("Password")]
		[DataType(DataType.Password)]
		public string UserPassword { get; set; }

		[DisplayName("Confirm Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[DisplayName("Education Qualification")]
		public string EducationQualification { get; set; }
	}
}
