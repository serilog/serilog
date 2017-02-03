using System.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.PerformanceTests.Support
{
    class NoOpMessageTemplateParser : IMessageTemplateParser
    {
        public static readonly NoOpMessageTemplateParser Instance = new NoOpMessageTemplateParser();

        static readonly MessageTemplate ConstTemplate = new MessageTemplate("text", Enumerable.Empty<MessageTemplateToken>());

        public MessageTemplate Parse(string messageTemplate) => ConstTemplate;
    }
}