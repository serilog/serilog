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
using System.Linq;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Core
{
    class Logger : ILogger, ILogEventSink
    {
        public const string SourceContextPropertyName = "SourceContext";

        private readonly IMessageTemplateCache _messageTemplateCache;
        private readonly LogEventLevel _minimumLevel;
        private readonly ILogEventSink _sink;
        private readonly ILogEventEnricher[] _enrichers;

        public Logger(IMessageTemplateCache messageTemplateCache, LogEventLevel minimumLevel, ILogEventSink sink, IEnumerable<ILogEventEnricher> enrichers)
        {
            if (sink == null) throw new ArgumentNullException("sink");
            if (enrichers == null) throw new ArgumentNullException("enrichers");
            _messageTemplateCache = messageTemplateCache;
            _minimumLevel = minimumLevel;
            _sink = sink;
            _enrichers = enrichers.ToArray();
        }

        public ILogger ForContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            var safeEnrichers = (enrichers ?? new ILogEventEnricher[0]).ToList();
            if (fixedProperties != null && fixedProperties.Length != 0)
            {
                safeEnrichers.Add(new FixedPropertyEnricher(fixedProperties));
            }

            return new Logger(_messageTemplateCache, _minimumLevel, this, safeEnrichers);
        }

        public ILogger ForContext(params LogEventProperty[] fixedProperties)
        {
            return ForContext(null, fixedProperties);
        }

        public ILogger ForContext<TSource>(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            var oldSize = fixedProperties.Length;
            // Odd signature - this is non-destructive/pure
            Array.Resize(ref fixedProperties, oldSize + 1);
            fixedProperties[oldSize] = LogEventProperty.For(SourceContextPropertyName, typeof(TSource).FullName);
            return ForContext(enrichers, fixedProperties);
        }

        public ILogger ForContext<TSource>(params LogEventProperty[] fixedProperties)
        {
            return ForContext<TSource>(null, fixedProperties);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Write(level, null, messageTemplate, propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (messageTemplate == null)
                return;

            if ((int)level < (int)_minimumLevel)
                return;

            // Catch a common pitfall when a single non-object array is cast to object[]
            // Needs some more thought
            if (propertyValues != null &&
                propertyValues.GetType() != typeof(object[]))
                propertyValues = new object[] { propertyValues };

            var now = DateTimeOffset.Now;
            var parsedTemplate = _messageTemplateCache.GetParsedTemplate(messageTemplate);
            var properties = parsedTemplate.ConstructPositionalProperties(propertyValues);

            var logEvent = new LogEvent(now, level, exception, messageTemplate, properties);
            Write(logEvent);
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return (int)level >= (int)_minimumLevel;
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) return;

            foreach (var enricher in _enrichers)
            {
                try
                {
                    enricher.Enrich(logEvent);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Exception {0} caught while enriching {1} with {2}.", ex, logEvent, enricher);
                }
            }

            _sink.Write(logEvent);
        }


        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Verbose(null, messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Debug(null, messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Information(null, messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Warning(null, messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Error(null, messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Fatal(null, messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
        }
    }
}
