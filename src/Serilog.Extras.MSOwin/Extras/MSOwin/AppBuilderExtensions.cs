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

using Owin;

namespace Serilog.Extras.MSOwin
{
    /// <summary>
    /// Extends <see cref="IAppBuilder"/> with support for installing Serilog middleware.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Open a nested diagnostic context for each request allowing the correlation of log messages per request.
        /// </summary>
        /// <param name="app">The IAppBuilder passed to your configuration method</param>
        /// <param name="propertyName">The property name the request Id is associated with. Default is <see cref="RequestContextMiddleware.DefaultRequestIdPropertyName"/></param>
        /// <returns>The original app parameter</returns>
        public static IAppBuilder UseSerilogRequestContext(this IAppBuilder app, string propertyName = RequestContextMiddleware.DefaultRequestIdPropertyName)
        {
            return app.Use(typeof(RequestContextMiddleware), new object[]{ propertyName });
        }
    }
}