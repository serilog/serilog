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
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Serilog.Core.Pipeline;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class TimedOperation : IDisposable
    {
        readonly ILogger _logger;
        readonly LogEventLevel _level;
        readonly TimeSpan? _warnIfExceeds;
        readonly object _identifier;
        readonly string _description;
        readonly Stopwatch _sw;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedOperation" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="description">A description for the operation.</param>
        /// <param name="level">The level used to write the timing operation details to the logger. By default this is the information level.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
        public TimedOperation(ILogger logger, LogEventLevel level, TimeSpan? warnIfExceeds, object identifier, string description)
        {
            _logger = logger;
            _level = level;
            _warnIfExceeds = warnIfExceeds;
            _identifier = identifier;
            _description = description;

            _logger.Write(_level, "Beginning operation {TimedOperationId}: {TimedOperationDescription}", _identifier, _description);

            _sw = Stopwatch.StartNew();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _sw.Stop();

            if (_warnIfExceeds.HasValue && _sw.Elapsed > _warnIfExceeds.Value)
                _logger.Write(LogEventLevel.Warning, "Operation {TimedOperationId}: {TimedOperationDescription} exceeded the limit of {WarningLimit} by completing in {TimedOperationElapsed}  ({TimedOperationElapsedInMs} ms)", _identifier, _description, _warnIfExceeds.Value, _sw.Elapsed, _sw.ElapsedMilliseconds);
            else
                _logger.Write(_level, "Completed operation {TimedOperationId}: {TimedOperationDescription} in {TimedOperationElapsed} ({TimedOperationElapsedInMs} ms)", _identifier, _description, _sw.Elapsed, _sw.ElapsedMilliseconds);
        }
    }
}