// Copyright 2014 Serilog Contributors
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

using System;
using Serilog.Events;
using Topshelf.Logging;

namespace Serilog.Extras.Topshelf
{
    class SerilogWriter : LogWriter
    {
        readonly ILogger _logger;
        const string ObjectMessageTemplate = "{Object}";

        public SerilogWriter(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            _logger = logger;
        }

        static LogEventLevel TopshelfToSerilogLevel(LoggingLevel level)
        {
            if (level == LoggingLevel.All)
                return LogEventLevel.Verbose;
            if (level == LoggingLevel.Debug)
                return LogEventLevel.Debug;
            if (level == LoggingLevel.Info)
                return LogEventLevel.Information;
            if (level == LoggingLevel.Warn)
                return LogEventLevel.Warning;
            if (level == LoggingLevel.Error)
                return LogEventLevel.Error;
            return LogEventLevel.Fatal;
        }

        public void Log(LoggingLevel level, object obj)
        {
            _logger.Write(TopshelfToSerilogLevel(level), ObjectMessageTemplate, obj);
        }

        public void Log(LoggingLevel level, object obj, Exception exception)
        {
            _logger.Write(TopshelfToSerilogLevel(level), exception, ObjectMessageTemplate, obj);
        }

        public void Log(LoggingLevel level, LogWriterOutputProvider messageProvider)
        {
            _logger.Write(TopshelfToSerilogLevel(level), ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void LogFormat(LoggingLevel level, IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Write(TopshelfToSerilogLevel(level), format, args);
        }

        public void LogFormat(LoggingLevel level, string format, params object[] args)
        {
            _logger.Write(TopshelfToSerilogLevel(level), format, args);
        }

        public void Debug(object obj)
        {
            _logger.Debug(ObjectMessageTemplate, obj);
        }

        public void Debug(object obj, Exception exception)
        {
            _logger.Debug(exception, ObjectMessageTemplate, obj);
        }

        public void Debug(LogWriterOutputProvider messageProvider)
        {
            _logger.Debug(ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        public void Info(object obj)
        {
            _logger.Information(ObjectMessageTemplate, obj);
        }

        public void Info(object obj, Exception exception)
        {
            _logger.Information(exception, ObjectMessageTemplate, obj);
        }

        public void Info(LogWriterOutputProvider messageProvider)
        {
            _logger.Information(ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Information(format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _logger.Information(format, args);
        }

        public void Warn(object obj)
        {
            _logger.Warning(ObjectMessageTemplate, obj);
        }

        public void Warn(object obj, Exception exception)
        {
            _logger.Warning(exception, ObjectMessageTemplate, obj);
        }

        public void Warn(LogWriterOutputProvider messageProvider)
        {
            _logger.Warning(ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Warning(format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warning(format, args);
        }

        public void Error(object obj)
        {
            _logger.Error(ObjectMessageTemplate, obj);
        }

        public void Error(object obj, Exception exception)
        {
            _logger.Error(exception, ObjectMessageTemplate, obj);
        }

        public void Error(LogWriterOutputProvider messageProvider)
        {
            _logger.Error(ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void Fatal(object obj)
        {
            _logger.Fatal(ObjectMessageTemplate, obj);
        }

        public void Fatal(object obj, Exception exception)
        {
            _logger.Fatal(exception, ObjectMessageTemplate, obj);
        }

        public void Fatal(LogWriterOutputProvider messageProvider)
        {
            _logger.Fatal(ObjectMessageTemplate, new MessageProvider(messageProvider));
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public bool IsDebugEnabled { get { return _logger.IsEnabled(LogEventLevel.Debug); } }
        public bool IsInfoEnabled { get { return _logger.IsEnabled(LogEventLevel.Information); } }
        public bool IsWarnEnabled { get { return _logger.IsEnabled(LogEventLevel.Warning); } }
        public bool IsErrorEnabled { get { return _logger.IsEnabled(LogEventLevel.Error); } }
        public bool IsFatalEnabled { get { return _logger.IsEnabled(LogEventLevel.Fatal); } }
    }
}
