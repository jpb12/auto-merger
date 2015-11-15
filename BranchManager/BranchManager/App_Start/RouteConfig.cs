﻿using System.Web.Mvc;
using System.Web.Routing;

namespace BranchManager
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}",
				defaults: new { controller = "Home", action = "Index" }
			);
		}
	}
}
