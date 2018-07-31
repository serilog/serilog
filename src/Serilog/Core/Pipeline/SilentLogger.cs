// Copyright 2013-2015 Serilog Contributors
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
using Serilog.Events;

namespace Serilog.Core.Pipeline
{
    class SilentLogger : ILogger
    {
        public static readonly ILogger Instance = new SilentLogger();

        private SilentLogger()
        {
        }

        public ILogger ForContext(ILogEventEnricher enricher)
        {
            return this;
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return this;
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return this;
        }

        public ILogger ForContext<TSource>()
        {
            return this;
        }

        public ILogger ForContext(Type source)
        {
            return this;
        }

        public void Write(LogEvent logEvent)
        {
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
        }

        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
        }

        public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return false;
        }

        public void Verbose(string messageTemplate)
        {
        }

        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Verbose(Exception exception, string messageTemplate)
        {
        }

        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(string messageTemplate)
        {
        }

        public void Debug<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(Exception exception, string messageTemplate)
        {
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(string messageTemplate)
        {
        }

        public void Information<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(Exception exception, string messageTemplate)
        {
        }

        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(string messageTemplate)
        {
        }

        public void Warning<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Exception exception, string messageTemplate)
        {
        }

        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(string messageTemplate)
        {
        }

        public void Error<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Exception exception, string messageTemplate)
        {
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(string messageTemplate)
        {
        }

        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
        }

        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Exception exception, string messageTemplate)
        {
        }

        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
        }

        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
        }

        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        [MessageTemplateFormatMethod("messageTemplate")]
        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
        {
            parsedTemplate = null;
            boundProperties = null;
            return false;
        }

        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            property = null;
            return false;
        }
    }
}
