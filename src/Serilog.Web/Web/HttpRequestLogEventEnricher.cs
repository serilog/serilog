using System;
using System.Threading;
using System.Web;

namespace Serilog.Web
{
    class HttpRequestLogEventEnricher : ILogEventEnricher
    {
        const string HttpRequestPropertyName = "HttpRequest";
        static int LastRequestId;
        static readonly string RequestIdItemName = typeof(HttpRequestLogEventEnricher).Name + "+RequestId";

        public void Enrich(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            
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
                LogEventProperty.For(HttpRequestPropertyName,
                new
                {
                    SessionId = sessionId,
                    Id = requestId,
                },
                true));
        }
    }
}
