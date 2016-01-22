using System;
using System.IO;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.TextWriter;

namespace Serilog
{
    public static class TextWriterLoggerConfigurationExtensions
    {
        /// <summary>
        /// Write log events to the provided <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="textWriter">The text writer to write log events to.</param>
        /// <param name="outputTemplate">Message template describing the output format.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration TextWriter(
            this LoggerSinkConfiguration sinkConfiguration,
            TextWriter textWriter,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = LoggerConfigurationExtensions.DefaultOutputTemplate,
            IFormatProvider formatProvider = null,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));
            if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            var sink = new TextWriterSink(textWriter, formatter);
            return sinkConfiguration.Sink(sink, restrictedToMinimumLevel, levelSwitch);
        }
    }
}