using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClaysysOnlineQuizTest
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		//protected void Application_BeginRequest()
		//{
		//	HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		//	HttpContext.Current.Response.Cache.SetNoStore();
		//	HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(-1));
		//	HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

		//	HttpContext.Current.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
		//	HttpContext.Current.Response.Headers["Pragma"] = "no-cache";
		//	HttpContext.Current.Response.Headers["Expires"] = "-1";
		//}
	}
}
