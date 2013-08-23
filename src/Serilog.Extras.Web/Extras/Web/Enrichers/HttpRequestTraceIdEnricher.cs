// Copyright 2013 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Web;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extras.Web.Enrichers
{
    /// <summary>
    /// Enrich log events with a HttpRequestTraceId GUID matching the
    /// RequestTraceIdentifier assigned by IIS and used throughout
    /// ASP.NET/ETW. IIS ETW tracing must be enabled for this to work.
    /// </summary>
    public class HttpRequestTraceIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HttpRequestTraceIdPropertyName = "HttpRequestTraceId";

        /// <summary>
        /// Enrich the log event with an id assigned to the currently-executing HTTP request, if any.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (HttpContext.Current == null)
                return;

            var serviceProvider = (IServiceProvider)HttpContext.Current;
            var workerReqest = (HttpWorkerRequest)serviceProvider.GetService(typeof(HttpWorkerRequest));
            var requestId = workerReqest.RequestTraceIdentifier;

            var requestIdProperty = new LogEventProperty(HttpRequestTraceIdPropertyName, new ScalarValue(requestId));
            logEvent.AddPropertyIfAbsent(requestIdProperty);
        }
    }
}
