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
using Elmah;
using Serilog.Core;
using Serilog.Events;
using ErrorLog = Elmah.Io.ErrorLog;

namespace Serilog.Sinks.ElmahIO
{
    /// <summary>
    /// Writes log events to the Elmah.IO service.
    /// </summary>
    public class ElmahIOSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        readonly ErrorLog _errorLog;

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account.
        /// </summary>
        ///  <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="logId">The log id as found on the Elmah.io website.</param>
        public ElmahIOSink(IFormatProvider formatProvider, Guid logId)
        {
            _formatProvider = formatProvider;
            _errorLog = new ErrorLog(logId);
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            var error = logEvent.Exception != null ? new Error(logEvent.Exception) : new Error();

            error.Message = logEvent.RenderMessage(_formatProvider);
            error.Time = logEvent.Timestamp.DateTime;

            _errorLog.Log(error);
        }
    }
}
