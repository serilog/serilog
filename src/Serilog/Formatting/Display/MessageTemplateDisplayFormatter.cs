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
using System.IO;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    class MessageTemplateTextFormatter : ITextFormatter
    {
        private readonly MessageTemplate _outputTemplate;
        readonly IMessageTemplateCache _messageTemplateCache;

        public MessageTemplateTextFormatter(string outputTemplate, IMessageTemplateCache messageTemplateCache)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            if (messageTemplateCache == null) throw new ArgumentNullException("messageTemplateCache");
            _messageTemplateCache = messageTemplateCache;
            _outputTemplate = _messageTemplateCache.GetParsedTemplate(outputTemplate);
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");
            var outputProperties = OutputProperties.GetOutputProperties(logEvent, _messageTemplateCache);
            _outputTemplate.Render(outputProperties, output);            
        }
    }
}
