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
        readonly string _anonymousUsername;
        readonly string _noneUsername;

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string UserNamePropertyName = "UserName";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNameEnricher"/> class.
        /// </summary>
        public UserNameEnricher()
            : this("(anonymous)", null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNameEnricher"/> class.
        /// </summary>
        /// <param name="anonymousUsername">The anonymous username. Leave null if you do not want to use anonymous user names. By default it is (anonymous).</param>
        /// <param name="noneUsername">The none username. If there is no username to be found, it will output this username. Leave null (default) to ignore non usernames.</param>
        public UserNameEnricher(string anonymousUsername = "(anonymous)", string noneUsername = null)
        {
            _anonymousUsername = anonymousUsername;
            _noneUsername = noneUsername;
        }

        /// <summary>
        /// Enrich the log event with the current ASP.NET user name, if User.Identity.IsAuthenticated is true.</summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) 
                throw new ArgumentNullException("logEvent");

            var userName = _noneUsername;

            if (HttpContext.Current != null)
            {
                var context = new HttpContextWrapper(HttpContext.Current);

                if (context.User != null)
                {
                    if (context.User.Identity == null || context.User.Identity.IsAuthenticated == false)
                    {
                        if (_anonymousUsername != null)
                            userName = _anonymousUsername;
                    }
                    else
                    {
                        userName = context.User.Identity.Name;
                    }
                }
            }

            if (userName == null) 
                return;

            var userNameProperty = new LogEventProperty(UserNamePropertyName, new ScalarValue(userName));
            logEvent.AddPropertyIfAbsent(userNameProperty);
        }
    }
}