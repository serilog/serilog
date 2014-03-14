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
    /// Enrich log events with the HttpSessionId property.
    /// </summary>
    public class HttpSessionIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HttpSessionIdPropertyName = "HttpSessionId";

        /// <summary>
        /// Enrich the log event with the current ASP.NET session id, if sessions are enabled.</summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session == null)
                return;

            var sessionId = HttpContext.Current.Session.SessionID;
            var sesionIdProperty = new LogEventProperty(HttpSessionIdPropertyName, new ScalarValue(sessionId));
            logEvent.AddPropertyIfAbsent(sesionIdProperty);
        }
    }
}
