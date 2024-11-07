using System;
using System.Web;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Filters
{
	public class UserAuthorizeAttribute : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			// Check if the user is a regular user
			return httpContext.Session["UserRole"] != null &&
				   httpContext.Session["UserRole"].ToString().Equals("User", StringComparison.OrdinalIgnoreCase);
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			// Redirect based on session status
			if (filterContext.HttpContext.Session["UserRole"] == null)
			{
				filterContext.Result = new RedirectResult("~/Login/Login");
			}
			else
			{
				filterContext.Result = new RedirectResult("~/Home/AccessDenied");
			}
		}
	}
}
