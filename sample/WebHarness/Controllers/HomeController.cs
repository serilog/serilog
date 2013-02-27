using System;
using System.Web.Mvc;
using Serilog;

namespace WebHarness.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            Log.Information("Hello from Index().");

            return View();
        }

        public ActionResult About()
        {
            throw new NotImplementedException("Maybe another day...");
        }
    }
}
