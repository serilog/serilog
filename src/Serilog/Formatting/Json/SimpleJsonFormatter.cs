using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;

namespace Serilog.Formatting.Json
{
    class SimpleJsonFormatter : ITextFormatter
    {
        readonly bool _omitEnclosingObject;
        readonly IDictionary<Type, Action<object, TextWriter>> _literalWriters;

        public SimpleJsonFormatter(bool omitEnclosingObject = false)
        {
            _omitEnclosingObject = omitEnclosingObject;

            _literalWriters = new Dictionary<Type, Action<object, TextWriter>>
            {
                { typeof(byte), WriteToString },
                { typeof(sbyte), WriteToString },
                { typeof(short), WriteToString },
                { typeof(ushort), WriteToString },
                { typeof(int), WriteToString },
                { typeof(uint), WriteToString },
                { typeof(long), WriteToString },
                { typeof(ulong), WriteToString },
                { typeof(float), WriteToString },
                { typeof(double), WriteToString },
                { typeof(decimal), WriteToString },
                { typeof(string), (v, w) => WriteString((string)v, w) },
                { typeof(DateTime), (v, w) => WriteDateTime((DateTime)v, w) },
                { typeof(DateTimeOffset), (v, w) => WriteOffset((DateTimeOffset)v, w) },
                { typeof(LogEventPropertyLiteralValue), (v, w) => WriteLiteral(((LogEventPropertyLiteralValue)v).Value, w) },
                { typeof(LogEventPropertySequenceValue), (v, w) => WriteSequence(((LogEventPropertySequenceValue)v).Elements, w) },
                { typeof(LogEventPropertyStructureValue), (v, w) => WriteStructure(((LogEventPropertyStructureValue)v).TypeTag, ((LogEventPropertyStructureValue)v).Properties, w) },
            };
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");

            if (!_omitEnclosingObject)
                output.Write("{");

            WriteJsonProperty("TimeStamp", logEvent.TimeStamp, output);
            WriteJsonProperty("Level", logEvent.Level, output);
            WriteJsonProperty("MessageTemplate", logEvent.MessageTemplate, output);

            if (logEvent.Exception != null)
                WriteJsonProperty("Exception", logEvent.Exception, output);

            if (logEvent.Properties.Count != 0)
            {
                output.Write("\"Properties\": {");
                foreach (var property in logEvent.Properties.Values)
                {
                    WriteJsonProperty(property.Name, property.Value, output);
                }
                output.Write("}, ");
            }

            if (!_omitEnclosingObject)
                output.Write("}");
        }

        void WriteStructure(string typeTag, LogEventProperty[] properties, TextWriter output)
        {
            throw new NotImplementedException();
        }

        void WriteSequence(IEnumerable elements, TextWriter output)
        {
            output.Write("[");
            foreach (var value in elements)
            {
                WriteLiteral(value, output);
                output.Write(", ");
            }
            output.Write("]");
        }

        void WriteJsonProperty(string name, object value, TextWriter output)
        {
            output.Write("\"");
            output.Write(name);
            output.Write("\": ");
            WriteLiteral(value, output);
            output.Write(", ");
        }

        void WriteLiteral(object value, TextWriter output)
        {
            if (value == null)
            {
                output.Write("null");
                return;
            }

            Action<object, TextWriter> writer;
            if (_literalWriters.TryGetValue(value.GetType(), out writer))
            {
                writer(value, output);
                return;
            }

            WriteString(value.ToString(), output);
        }

        static void WriteToString(object number, TextWriter output)
        {
            output.Write(number.ToString());
        }

        static void WriteOffset(DateTimeOffset value, TextWriter output)
        {
            output.Write("\"");
            output.Write(value.ToString("o"));
            output.Write("\"");
        }

        static void WriteDateTime(DateTime value, TextWriter output)
        {
            output.Write("\"");
            output.Write(value.ToString("o"));
            output.Write("\"");
        }

        static void WriteString(string value, TextWriter output)
        {
            var content = value.Replace("\"", "\\\"");
            output.Write("\"");
            output.Write(content);
            output.Write("\"");
        }
    }
}
