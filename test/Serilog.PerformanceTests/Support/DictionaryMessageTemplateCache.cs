using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.PerformanceTests.Support
{
    class DictionaryMessageTemplateCache : IMessageTemplateParser
    {
        readonly IMessageTemplateParser _innerParser;
        readonly object _templatesLock = new object();

        readonly Dictionary<string, MessageTemplate> _templates = new Dictionary<string, MessageTemplate>();

        const int MaxCacheItems = 1000;
        const int MaxCachedTemplateLength = 1024;

        public DictionaryMessageTemplateCache(IMessageTemplateParser innerParser)
        {
            _innerParser = innerParser ?? throw new ArgumentNullException(nameof(innerParser));
        }

        public MessageTemplate Parse(string messageTemplate)
        {
            if (messageTemplate is null) throw new ArgumentNullException(nameof(messageTemplate));

            if (messageTemplate.Length > MaxCachedTemplateLength)
                return _innerParser.Parse(messageTemplate);

            MessageTemplate result;
            lock (_templatesLock)
                if (_templates.TryGetValue(messageTemplate, out result))
                    return result;

            result = _innerParser.Parse(messageTemplate);

            lock (_templatesLock)
            {
                // Exceeding MaxCacheItems is *not* the sunny day scenario; all we're doing here is preventing out-of-memory
                // conditions when the library is used incorrectly. Correct use (templates, rather than
                // direct message strings) should barely, if ever, overflow this cache.

                // Changing workloads through the lifecycle of an app instance mean we can gain some ground by
                // potentially dropping templates generated only in startup, or only during specific infrequent
                // activities.

                if (_templates.Count == MaxCacheItems)
                    _templates.Clear();

                _templates[messageTemplate] = result;
            }

            return result;
        }
    }
}
