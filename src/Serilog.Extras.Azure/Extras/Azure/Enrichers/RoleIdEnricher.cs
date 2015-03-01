using System;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.Azure.Enrichers
{
    /// <summary>
    /// Enrich log events with the <see cref="RoleEnvironment.CurrentRoleInstance"/>'s Id property when available in the Azure <see cref="RoleEnvironment"/>.
    /// </summary>
    public class RoleIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string RoleIdPropertyName = "AzureRoleId";

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

            string roleId = null;
            if (RoleEnvironment.IsAvailable && RoleEnvironment.CurrentRoleInstance != null && RoleEnvironment.CurrentRoleInstance.Id != null)
            {
                roleId = RoleEnvironment.CurrentRoleInstance.Id;
            }

            if (roleId == null)
                return;

            var roleIdProperty = new LogEventProperty(RoleIdPropertyName, new ScalarValue(roleId));
            logEvent.AddPropertyIfAbsent(roleIdProperty);
        }

        #endregion
    }
}