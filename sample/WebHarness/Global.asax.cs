using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Serilog;
using Serilog.Extras.Web.Enrichers;
using WebHarness.Controllers;

namespace WebHarness
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ConfigureLog();

            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        void ConfigureLog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace(outputTemplate: "{Timestamp} [{Level}] ({HttpRequestId}) {Message:l}{NewLine:l}{Exception:l}")
                .WriteTo.Glimpse()
                .WriteTo.SignalR(GlobalHost.ConnectionManager.GetHubContext<LogHub>())
                .Enrich.With<HttpRequestIdEnricher>()
                .CreateLogger();
        }
    }
}