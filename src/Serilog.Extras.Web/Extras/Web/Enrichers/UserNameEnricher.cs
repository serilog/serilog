using System;
using System.Web;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.Web.Enrichers
{
    /// <summary>
    /// Enrich log events with the UserName property when available in the HttpContext.
    /// </summary>
    public class UserNameEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string UserNamePropertyName = "UserName";

        /// <summary>
        /// Enrich the log event with the current ASP.NET user name, if User.Identity.IsAuthenticated is true.</summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (HttpContext.Current == null)
                return;

            var context = new HttpContextWrapper(HttpContext.Current);

            if (context.User == null)
                return;

            if (context.User.Identity.IsAuthenticated == false)
                return;

            var userName = context.User.Identity.Name;
            var userNameProperty = new LogEventProperty(UserNamePropertyName, new ScalarValue(userName));
            logEvent.AddPropertyIfAbsent(userNameProperty);
        }
    }
}