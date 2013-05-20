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
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Core
{
    class SilentLogger : ILogger
    {
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

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Write(LogEventLevel level, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Write(LogEventLevel level, Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return false;
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Verbose(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Verbose(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, string messageTemplate, params object[] propertyValues)
        {
        }
    }
}
