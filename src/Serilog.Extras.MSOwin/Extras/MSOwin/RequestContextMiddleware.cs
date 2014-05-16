// Copyright 2014 Serilog Contributors
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Context;

namespace Serilog.Extras.MSOwin
{
    /// <summary>
    /// Adds a RequestId property to the logging context during request processing.
    /// </summary>
    public class RequestContextMiddleware
    {
        /// <summary>
        /// The property name carrying the request ID.
        /// </summary>
        public const string DefaultRequestIdPropertyName = "RequestId";

        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly string _propertyName;

        /// <summary>
        /// Construct the middleware.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="propertyName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RequestContextMiddleware(Func<IDictionary<string, object>, Task> next, string propertyName = DefaultRequestIdPropertyName)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            _next = next;
            _propertyName = string.IsNullOrWhiteSpace(propertyName) ? DefaultRequestIdPropertyName : propertyName;
        }

        /// <summary>
        /// Process a request.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public async Task Invoke(IDictionary<string, object> environment)
        {
            // There is not yet a standard way to uniquely identify and correlate an owin request
            // ... hence 'RequestId' https://github.com/owin/owin/issues/21
            using (LogContext.PushProperty(_propertyName, Guid.NewGuid()))
            {
                await _next(environment);
            }
        }
    }
}