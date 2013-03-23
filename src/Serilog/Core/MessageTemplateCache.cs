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
using System.Collections.Concurrent;
using Serilog.Parsing;

namespace Serilog.Core
{
    class MessageTemplateCache : IMessageTemplateCache
    {
        readonly ConcurrentDictionary<string, MessageTemplate> _templates = new ConcurrentDictionary<string,MessageTemplate>();

        public MessageTemplate GetParsedTemplate(string messageTemplate)
        {
            if (messageTemplate == null) throw new ArgumentNullException("messageTemplate");
            return _templates.GetOrAdd(messageTemplate, Parse);
        }

        static MessageTemplate Parse(string messageTemplate)
        {
            return new MessageTemplate(MessageTemplateParser.Parse(messageTemplate));
        }
    }
}
