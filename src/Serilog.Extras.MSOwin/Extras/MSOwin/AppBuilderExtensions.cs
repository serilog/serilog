using Owin;

// ReSharper disable once CheckNamespace
namespace Serilog.Extras.MSOwin
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Open a nested diagnostic context for each request allowing the correlation of log messages per request.
        /// </summary>
        /// <param name="app">The IAppBuilder passed to your configuration method</param>
        /// <param name="propertyName">The property name the request Id is associated with. Defaut is <see cref="RequestContextMiddleware.DefaultRequestIdPropertyName"/></param>
        /// <returns>The original app parameter</returns>
        public static IAppBuilder UseSerilogRequestContext(this IAppBuilder app, string propertyName = RequestContextMiddleware.DefaultRequestIdPropertyName)
        {
            return app.Use(typeof(RequestContextMiddleware), new object[]{ propertyName });
        }
    }
}