using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Core;

namespace Serilog.Sinks
{
    static class OutputProperties
    {
        public const string MessagePropertyName = "Message";
        public const string TimeStampPropertyName = "TimeStamp";
        public const string LevelPropertyName = "Level";
        public const string NewLinePropertyName = "NewLine";

        public static IReadOnlyDictionary<string, LogEventProperty> GetOutputProperties(LogEvent logEvent, IMessageTemplateRepository messageTemplateRepository)
        {
            var result = logEvent.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var messageTemplate = messageTemplateRepository.GetParsedTemplate(logEvent.MessageTemplate);

            result.Add(MessagePropertyName, new LogEventProperty(MessagePropertyName, new LogEventPropertyMessageValue(messageTemplate, logEvent.Properties)));
            result.Add(TimeStampPropertyName, LogEventProperty.For(TimeStampPropertyName, logEvent.TimeStamp));
            result.Add(LevelPropertyName, new LogEventProperty(LevelPropertyName, new LogEventPropertyTokenValue(logEvent.Level.ToString())));
            result.Add(NewLinePropertyName, new LogEventProperty(NewLinePropertyName, new LogEventPropertyTokenValue(Environment.NewLine)));

            return result;
        }
    }
}
