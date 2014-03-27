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
using System.Diagnostics;
using System.Threading;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Extras.Timing;
using Serilog.Formatting.Display;
using Serilog.Formatting.Raw;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework 
    /// capabilities.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Begins an operation by placing the code to be timed inside a using block. 
        /// When the block is being exited, the time it took is logged.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="description">A description for this operation.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
        /// <returns>A disposable object. Wrap this inside a using block so the dispose can be called to stop the timing.</returns>
        public static IDisposable BeginTimedOperation(
            this ILogger logger,  
            string description,
            string identifier = null,   
            LogEventLevel level = LogEventLevel.Information,
            TimeSpan? warnIfExceeds = null)
        {
            if (string.IsNullOrEmpty(identifier))
                identifier = Guid.NewGuid().ToString();

            return new TimedOperation(logger, level, warnIfExceeds, identifier, description);
        }

       
    }
}
