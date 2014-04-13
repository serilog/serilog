using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Context;

namespace Serilog.Extras.MSOwin
{
    public class RequestContextMiddleware
    {
        public const string DefaultRequestIdPropertyName = "RequestId";

        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly string _propertyName;

        public RequestContextMiddleware(Func<IDictionary<string, object>, Task> next, string propertyName = DefaultRequestIdPropertyName)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            _next = next;
            _propertyName = string.IsNullOrWhiteSpace(propertyName) ? DefaultRequestIdPropertyName : propertyName;
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            // There is not yet a standard way to uniquely identify and correlate an owin request
            // ... hence 'RequestId' https://github.com/owin/owin/issues/21
            using (LogContext.PushProperty(_propertyName, Guid.NewGuid()))
            {
                await _next(enviroment);
            }
        }
    }
}