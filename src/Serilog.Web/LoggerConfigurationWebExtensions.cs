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
using Serilog.Web;

namespace Serilog
{
    /// <summary>
    /// Extend the <see cref="LoggerConfiguration"/> DSL with web-related methods.
    /// </summary>
    public static class LoggerConfigurationWebExtensions
    {
        /// <summary>
        /// Enrich log events in the specified configuration with information
        /// about the current ASP.NET HTTP request.
        /// </summary>
        /// <param name="loggerConfiguration">The configuration to modify.</param>
        /// <returns>Configuration object allowing configuration to continue.</returns>
        public static LoggerConfiguration EnrichedWithHttpRequestProperties(this LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            return loggerConfiguration.EnrichedBy(new HttpRequestLogEventEnricher());
        }
    }
}
