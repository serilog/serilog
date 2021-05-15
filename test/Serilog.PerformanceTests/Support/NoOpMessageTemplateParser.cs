using System.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.PerformanceTests.Support
{
    class NoOpMessageTemplateParser : IMessageTemplateParser
    {
        public static readonly NoOpMessageTemplateParser Instance = new();

        static readonly MessageTemplate ConstTemplate = new("text", Enumerable.Empty<MessageTemplateToken>());

        public MessageTemplate Parse(string messageTemplate) => ConstTemplate;
    }
}