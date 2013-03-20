using System;
using System.IO;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Formatting.Json
{
    class SimpleJsonFormatter : ITextFormatter
    {
        readonly bool _omitEnclosingObject;

        public SimpleJsonFormatter(bool omitEnclosingObject = false)
        {
            _omitEnclosingObject = omitEnclosingObject;
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");

            if (!_omitEnclosingObject)
                output.Write("{");

            WriteOffset("TimeStamp", logEvent.TimeStamp, output);
            WriteString("Level", logEvent.Level.ToString(), output);
            WriteString("MessageTemplate", logEvent.MessageTemplate, output, escape: true);

            if (logEvent.Exception != null)
                WriteString("Exception", logEvent.Exception.ToString(), output, escape: true);

            if (logEvent.Properties.Count != 0)
            {
                output.Write("\"Properties\":{");
                foreach (var property in logEvent.Properties.Values)
                {
                    WriteProperty(property, output);
                }
                output.Write("},");
            }

            if (!_omitEnclosingObject)
                output.Write("}");
        }

        static void WriteProperty(LogEventProperty property, TextWriter output)
        {
            var lp = property.Value as LogEventPropertyLiteralValue;
            if (lp != null)
            {
                WriteLiteral(property.Name, lp.Value, output);
            }
            else
            {
                var sqp = property.Value as LogEventPropertySequenceValue;
                if (sqp != null)
                {
                    WriteSequence(property.Name, sqp.Elements, output);
                }
                else
                {
                    var stp = property.Value as LogEventPropertyStructureValue;
                    if (stp != null)
                    {
                        WriteStructure(property.Name, stp, output);
                    }
                    else
                    {
                        SelfLog.WriteLine("Unsupported property value type {0}", property.Value.GetType());
                        output.Write("null");
                    }
                }
            }
        }

        static void WriteOffset(string name, DateTimeOffset value, TextWriter output)
        {
            WriteString(name, value.ToString("o"), output);
        }

        static void WriteString(string name, string value, TextWriter output, bool escape = false)
        {
            var content = escape ? JsonEscape(value) : value;
            output.Write("\"");
            output.Write(name);
            output.Write("\":\"");
            output.Write(content);
            output.Write("\",");
        }

        static string JsonEscape(string value)
        {
            return value.Replace("\"", "\\\"");
        }
    }
}
