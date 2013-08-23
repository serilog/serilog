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
using System.Threading;
using System.Web;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Web.Enrichers
{
    /// <summary>
    /// Enrich log events with a HttpRequestNumber unique within the current
    /// logging session.
    /// </summary>
    public class HttpRequestNumberEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HttpRequestNumberPropertyName = "HttpRequestNumber";

        static int LastRequestNumber;
        static readonly string RequestNumberItemName = typeof(HttpRequestNumberEnricher).Name + "+RequestNumber";

        /// <summary>
        /// Enrich the log event with the number assigned to the currently-executing HTTP request, if any.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (HttpContext.Current == null)
                return;

            int requestNumber;
            var requestNumberItem = HttpContext.Current.Items[RequestNumberItemName];
            if (requestNumberItem == null)
                HttpContext.Current.Items[RequestNumberItemName] = requestNumber = Interlocked.Increment(ref LastRequestNumber);
            else
                requestNumber = (int)requestNumberItem;

            var requestNumberProperty = new LogEventProperty(HttpRequestNumberPropertyName, new ScalarValue(requestNumber));
            logEvent.AddPropertyIfAbsent(requestNumberProperty);
        }
    }
}
