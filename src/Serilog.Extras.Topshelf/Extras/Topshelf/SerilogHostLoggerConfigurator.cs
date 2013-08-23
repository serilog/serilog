// Copyright 2013 Serilog Contributors
// Based on Topshelf.Log4Net, copyright 2007-2012 Chris Patterson,
// Dru Sellers, Travis Smith, et. al.
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

using Topshelf.Logging;

namespace Serilog.Extras.Topshelf
{
    /// <summary>
    /// Configures the Topshelf host to write log messages to Serilog.
    /// </summary>
    public class SerilogHostLoggerConfigurator : HostLoggerConfigurator
    {
        readonly ILogger _logger;

        /// <summary>
        /// Construct the host configurator.
        /// </summary>
        /// <param name="logger">Optional logger instance for the
        /// configurator to use. If this is omitted, the default <see cref="Log"/>
        /// logger will be used.</param>
        public SerilogHostLoggerConfigurator(ILogger logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create the factory that the host will use to write
        /// messages.
        /// </summary>
        /// <returns>The factory.</returns>
        public LogWriterFactory CreateLogWriterFactory()
        {
            return new SerilogWriterFactory(_logger);
        }
    }
}
