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
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using log4net;

namespace Serilog.Sinks.Log4Net
{
    class Log4NetSink : ILogEventSink
    {
        readonly string _defaultLoggerName;
        readonly IFormatProvider _formatProvider;

        public Log4NetSink(string defaultLoggerName, IFormatProvider formatProvider = null)
        {
            if (defaultLoggerName == null) throw new ArgumentNullException("defaultLoggerName");
            _defaultLoggerName = defaultLoggerName;
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var loggerName = _defaultLoggerName;

            LogEventPropertyValue sourceContext;
            if (logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out sourceContext))
            {
                var sv = sourceContext as ScalarValue;
                if (sv != null && sv.Value is string)
                    loggerName = (string)sv.Value;
            }

            var message = logEvent.RenderMessage(_formatProvider);
            var exception = logEvent.Exception;

            var logger = LogManager.GetLogger(loggerName);
            switch (logEvent.Level)
            {
                case LogEventLevel.Verbose:
                case LogEventLevel.Debug:
                    logger.Debug(message, exception);
                    break;
                case LogEventLevel.Information:
                    logger.Info(message, exception);
                    break;
                case LogEventLevel.Warning:
                    logger.Warn(message, exception);
                    break;
                case LogEventLevel.Error:
                    logger.Error(message, exception);
                    break;
                case LogEventLevel.Fatal:
                    logger.Fatal(message, exception);
                    break;
                default:
                    SelfLog.WriteLine("Unexpected logging level, writing to log4net as Info");
                    logger.Info(message, exception);
                    break;
            }
        }
    }
}
