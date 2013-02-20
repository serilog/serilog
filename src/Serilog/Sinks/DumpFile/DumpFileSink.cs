using System;
using System.IO;
using Serilog.Core;

namespace Serilog.Sinks.DumpFile
{
    class DumpFileSink : ILogEventSink
    {
        readonly TextWriter _output;

        public DumpFileSink(string path)
        {
            _output = new StreamWriter(File.OpenWrite(path));
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            _output.Write("[" + logEvent.TimeStamp + "] " + logEvent.Level + ": \"");
            _output.Write(logEvent.MessageTemplate);
            _output.WriteLine("\"");
            if (logEvent.Exception != null)
                _output.WriteLine(logEvent.Exception);
            foreach (var property in logEvent.Properties.Values)
            {
                _output.Write(property.Name + " = ");
                property.Value.Render(_output);
                _output.WriteLine();
            }
            _output.WriteLine();
            _output.Flush();
        }
    }
}
