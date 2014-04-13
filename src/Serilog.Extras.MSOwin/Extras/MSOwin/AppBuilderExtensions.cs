using Owin;

// ReSharper disable once CheckNamespace
namespace Serilog.Extras.MSOwin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseSerilogRequestContext(this IAppBuilder app)
        {
            return app.Use(typeof(RequestContextMiddleware));
        }
    }
}