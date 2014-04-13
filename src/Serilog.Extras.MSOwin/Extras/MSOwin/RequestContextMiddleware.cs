using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Context;

namespace Serilog.Extras.MSOwin
{
    public class RequestContextMiddleware
    {
        public const string RequestIdPropertyName = "RequestId";

        private readonly Func<IDictionary<string, object>, Task> _next;

        public RequestContextMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            // There is not yet a standard way to uniquely identify and correlate an owin request
            // ... hence 'RequestId' https://github.com/owin/owin/issues/21
            using (LogContext.PushProperty(RequestIdPropertyName, Guid.NewGuid()))
            {
                await _next(enviroment);
            }
        }
    }
}