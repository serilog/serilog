using System;
using System.Web;

namespace Serilog.Web
{
    public class ApplicationLifecycleModule : IHttpModule
    {
        public static void Register()
        {
            HttpApplication.RegisterModule(typeof(ApplicationLifecycleModule));
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.Error += Error;
        }

        static void Error(object sender, EventArgs e)
        {
            var ex = ((HttpApplication)sender).Server.GetLastError();
            Log.Error(ex, "Error caught in global handler.");
        }

        static void BeginRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            Log.Information("Beginning HTTP request for {RawUrl}.", request.RawUrl);
        }

        public void Dispose()
        {
        }
    }
}
