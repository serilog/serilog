using System;
using System.Globalization;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System.Threading.Tasks;

namespace Serilog.Tests.Support
{
    public class StringSink : ILogEventSink
    {
        readonly StringWriter _sw = new StringWriter();
        readonly ITextFormatter _formatter;

        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

        public StringSink(string outputTemplate = DefaultOutputTemplate)
        {
            _formatter = new MessageTemplateTextFormatter(outputTemplate, CultureInfo.InvariantCulture);
        }

        public Task Emit(LogEvent logEvent)
        {
            _formatter.Format(logEvent, _sw);
            return Task.FromResult((object)null);
        }

        public override string ToString()
        {
            return _sw.ToString();
        }
    }
}
