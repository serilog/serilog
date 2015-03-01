using System;
using Microsoft.WindowsAzure.ServiceRuntime;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.Azure.Enrichers
{
    /// <summary>
    /// Enrich log events with the <see cref="RoleEnvironment.DeploymentId"/> property when available in the Azure <see cref="RoleEnvironment"/>.
    /// </summary>
    public class DeploymentIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string DeploymentIdPropertyName = "AzureDeploymentId";

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

            string deploymentId = null;
            if (RoleEnvironment.IsAvailable && RoleEnvironment.DeploymentId != null)
            {
                deploymentId = RoleEnvironment.DeploymentId;
            }

            if (deploymentId == null)
                return;

            var deploymentIdProperty = new LogEventProperty(DeploymentIdPropertyName, new ScalarValue(deploymentId));
            logEvent.AddPropertyIfAbsent(deploymentIdProperty);
        }

        #endregion
    }
}