using System;
using System.Web;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest.Filters
{
	public class AdminAuthorizeAttribute : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return httpContext.Session["UserRole"] != null &&
				   httpContext.Session["UserRole"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase);
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
