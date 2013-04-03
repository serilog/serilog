// Copyright 2013 Nicholas Blumhardt
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

namespace Serilog.Web
{
    class HttpRequestLogEventEnricher : ILogEventEnricher
    {
        const string HttpRequestPropertyName = "HttpRequest";
        static int LastRequestId;
        static readonly string RequestIdItemName = typeof(HttpRequestLogEventEnricher).Name + "+RequestId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (propertyFactory == null) throw new ArgumentNullException("propertyFactory");

            if (HttpContext.Current == null)
                return;

            int requestId;
            var requestIdItem = HttpContext.Current.Items[RequestIdItemName];
            if (requestIdItem == null)
                HttpContext.Current.Items[RequestIdItemName] = requestId = Interlocked.Increment(ref LastRequestId);
            else
                requestId = (int)requestIdItem;

            string sessionId = null;
            if (HttpContext.Current.Session != null)
                sessionId = HttpContext.Current.Session.SessionID;

            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty(HttpRequestPropertyName,
                new
                {
                    SessionId = sessionId,
                    Id = requestId,
                },
                destructureObjects: true));
        }
    }
}
