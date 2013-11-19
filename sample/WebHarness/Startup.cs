
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebHarness.Startup))]
namespace WebHarness
{
   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
