using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    static class OutputProperties
    {
        public const string MessagePropertyName = "Message";
        public const string TimeStampPropertyName = "TimeStamp";
        public const string LevelPropertyName = "Level";
        public const string NewLinePropertyName = "NewLine";
        public const string ExceptionPropertyName = "Exception";

        public static IReadOnlyDictionary<string, LogEventProperty> GetOutputProperties(LogEvent logEvent, IMessageTemplateCache messageTemplateCache)
        {
            var result = logEvent.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var messageTemplate = messageTemplateCache.GetParsedTemplate(logEvent.MessageTemplate);

            result.Add(MessagePropertyName, new LogEventProperty(MessagePropertyName, new LogEventPropertyMessageValue(messageTemplate, logEvent.Properties)));
            result.Add(TimeStampPropertyName, LogEventProperty.For(TimeStampPropertyName, logEvent.TimeStamp));
            result.Add(LevelPropertyName, LogEventProperty.For(LevelPropertyName, logEvent.Level));
            result.Add(NewLinePropertyName, new LogEventProperty(NewLinePropertyName, new LogEventPropertyLiteralValue(Environment.NewLine)));

            var exception = logEvent.Exception == null ? "" : (logEvent.Exception + Environment.NewLine);
            result.Add(ExceptionPropertyName, LogEventProperty.For(ExceptionPropertyName, exception));

            return result;
        }
    }
}
