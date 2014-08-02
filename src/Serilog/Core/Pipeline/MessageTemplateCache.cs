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
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Core.Pipeline
{
    using System.Threading;

    class MessageTemplateCache : IMessageTemplateParser
    {
        readonly IMessageTemplateParser _innerParser;
        readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        readonly Dictionary<string, MessageTemplate> _templates = new Dictionary<string, MessageTemplate>();

        const int MaxCacheItems = 1000;

        public MessageTemplateCache(IMessageTemplateParser innerParser)
        {
            if (innerParser == null) throw new ArgumentNullException("innerParser");
            _innerParser = innerParser;
        }

        public MessageTemplate Parse(string messageTemplate)
        {
            if (messageTemplate == null)
            {
                throw new ArgumentNullException("messageTemplate");
            }
            
            MessageTemplate value;
            try
            {
                _locker.EnterReadLock();
                if (_templates.TryGetValue(messageTemplate, out value))
                {
                    return value;
                }
            }
            finally
            {
                _locker.ExitReadLock();
            }

            value = _innerParser.Parse(messageTemplate);

            // Exceeding MaxCacheItems is *not* the sunny day scenario; all we're doing here is preventing out-of-memory
            // conditions when the library is used incorrectly. Correct use (templates, rather than
            // direct message strings) should barely, if ever, overflow this cache.
            if (_templates.Count > MaxCacheItems)
            {
                return value;
            }
            _locker.EnterWriteLock();
            try
            {
                _templates[messageTemplate] = value;
            }
            finally
            {
                _locker.ExitWriteLock();
            }
            return value;
        }

        public void Dispose()
        {
            if (_locker != null)
            {
                _locker.Dispose();
            }
            if (_innerParser != null)
            {
                _innerParser.Dispose();
            }
        }
    }
}
