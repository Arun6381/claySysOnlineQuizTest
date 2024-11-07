using System.Web;
using System.Web.Mvc;

namespace ClaysysOnlineQuizTest
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
