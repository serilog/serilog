using System;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.Azure.Enrichers
{
    /// <summary>
    /// Enrich log events with the <see cref="Role.Name"/> property when available in the Azure <see cref="RoleEnvironment"/>.
    /// </summary>
    public class RoleNameEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string RoleNamePropertyName = "AzureRoleName";

        #region Implementation of ILogEventEnricher

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null)
                throw new ArgumentNullException("logEvent");

            string roleName = null;
            if (RoleEnvironment.IsAvailable && RoleEnvironment.CurrentRoleInstance != null && RoleEnvironment.CurrentRoleInstance.Role != null)
            {
                roleName = RoleEnvironment.CurrentRoleInstance.Role.Name;
            }

            if (roleName == null)
                return;

            var roleNameProperty = new LogEventProperty(RoleNamePropertyName, new ScalarValue(roleName));
            logEvent.AddPropertyIfAbsent(roleNameProperty);
        }

        #endregion
    }
}
