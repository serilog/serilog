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
