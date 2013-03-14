using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MusicSite.Models;

namespace MusicSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "",
                "{controller}/{action}/{param}",
                new { controller = "Home", action = "Index", param = UrlParameter.Optional }
            );

        }

        protected void Application_Start()
        {
            Database.SetInitializer<DatabaseContext>(new DatabaseInitializer());
            AreaRegistration.RegisterAllAreas();

            // Использовать LocalDB для Entity Framework по умолчанию
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}