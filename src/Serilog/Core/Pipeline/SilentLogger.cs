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
using System.Threading.Tasks;
using Serilog.Events;

namespace Serilog.Core.Pipeline
{
    class SilentLogger : ILogger
    {
        private Task completedTask = Task.FromResult((object)null);

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

        public Task Write(LogEvent logEvent)
        {
            return completedTask;
        }

        public Task Write(LogEventLevel level, string messageTemplate)
        {
            return completedTask;
        }

        public Task Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return false;
        }

        public Task Verbose(string messageTemplate)
        {
            return completedTask;
        }

        public Task Verbose<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Verbose(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Verbose(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Debug(string messageTemplate)
        {
            return completedTask;
        }

        public Task Debug<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Debug(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Debug(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Information(string messageTemplate)
        {
            return completedTask;
        }

        public Task Information<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Information(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Information(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Warning(string messageTemplate)
        {
            return completedTask;
        }

        public Task Warning<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Warning(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Warning(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Error(string messageTemplate)
        {
            return completedTask;
        }

        public Task Error<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Error(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Error(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Fatal(string messageTemplate)
        {
            return completedTask;
        }

        public Task Fatal<T>(string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Fatal(string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
        }

        public Task Fatal(Exception exception, string messageTemplate)
        {
            return completedTask;
        }

        public Task Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            return completedTask;
        }

        public Task Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            return completedTask;
        }

        public Task Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            return completedTask;
        }

        public Task Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            return completedTask;
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