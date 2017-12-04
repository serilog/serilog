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
using Serilog.Events;

#if HASHTABLE
using System.Collections;
#else
using System.Collections.Generic;
#endif

namespace Serilog.Core.Pipeline
{
    class MessageTemplateCache : IMessageTemplateParser
    {
        readonly IMessageTemplateParser _innerParser;
        readonly object _templatesLock = new object();

#if HASHTABLE
        readonly Hashtable _templates = new Hashtable();
#else
        readonly Dictionary<string, MessageTemplate> _templates = new Dictionary<string, MessageTemplate>();
#endif

        const int MaxCacheItems = 1000;
        const int MaxCachedTemplateLength = 1024;

        public MessageTemplateCache(IMessageTemplateParser innerParser)
        {
            if (innerParser == null) throw new ArgumentNullException(nameof(innerParser));
            _innerParser = innerParser;
        }

        public MessageTemplate Parse(string messageTemplate)
        {
            if (messageTemplate == null) throw new ArgumentNullException(nameof(messageTemplate));

            if (messageTemplate.Length > MaxCachedTemplateLength)
                return _innerParser.Parse(messageTemplate);

#if HASHTABLE
            // ReSharper disable once InconsistentlySynchronizedField
            // ignored warning because this is by design
            var result = (MessageTemplate)_templates[messageTemplate];
            if (result != null)
                return result;
#else
            MessageTemplate result;
            lock(_templatesLock)
                if (_templates.TryGetValue(messageTemplate, out result))
                    return result;
#endif

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
