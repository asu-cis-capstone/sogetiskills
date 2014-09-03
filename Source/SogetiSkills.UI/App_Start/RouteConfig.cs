using System.Web.Routing;
using AttributeRouting.Web.Mvc;
using SogetiSkills.UI.Controllers;

namespace SogetiSkills.UI 
{
    public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes) 
		{
            routes.MapAttributeRoutes(config =>
                {
                    config.AddRoutesFromAssemblyOf<HomeController>();
                    config.AppendTrailingSlash = true;
                    config.UseLowercaseRoutes = true;
                });
		}

        public static void Start() 
		{
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
