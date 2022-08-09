using System;
using System.Collections.Concurrent;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.PerformanceTests.Support;

class ConcurrentDictionaryMessageTemplateCache : IMessageTemplateParser
{
    readonly IMessageTemplateParser _innerParser;

    readonly ConcurrentDictionary<string, MessageTemplate> _templates = new();

    const int MaxCacheItems = 1000;
    const int MaxCachedTemplateLength = 1024;

    public ConcurrentDictionaryMessageTemplateCache(IMessageTemplateParser innerParser)
    {
        _innerParser = innerParser ?? throw new ArgumentNullException(nameof(innerParser));
    }

    public MessageTemplate Parse(string messageTemplate)
    {
        if (messageTemplate == null) throw new ArgumentNullException(nameof(messageTemplate));

        if (messageTemplate.Length > MaxCachedTemplateLength)
            return _innerParser.Parse(messageTemplate);

        if (_templates.TryGetValue(messageTemplate, out var result))
            return result;

        result = _innerParser.Parse(messageTemplate);

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
