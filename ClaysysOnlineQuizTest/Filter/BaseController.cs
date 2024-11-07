using System.Web.Mvc;
using System.Web;
using System;

public class BaseController : Controller
{
	public void PerformCacheAndSessionCheck(ActionExecutingContext filterContext)
	{
		HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
		HttpContext.Response.Cache.SetNoStore();

		if (Session["Uemail"] == null && Session["Aemail"] == null)
		{
			filterContext.Result = new RedirectToRouteResult(
				new System.Web.Routing.RouteValueDictionary(
					new { controller = "Login", action = "Login" }
				)
			);
		}
	}


	protected override void OnActionExecuting(ActionExecutingContext filterContext)
	{
		PerformCacheAndSessionCheck(filterContext);
		base.OnActionExecuting(filterContext);
	}
}