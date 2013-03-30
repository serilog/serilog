// Copyright 2013 Nicholas Blumhardt
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;

namespace Serilog.Formatting.Json
{
    /// <summary>
    /// Formats log events in a simple JSON structure.
    /// </summary>
    public class SimpleJsonFormatter : ITextFormatter
    {
        readonly bool _omitEnclosingObject;
        readonly IDictionary<Type, Action<object, TextWriter>> _literalWriters;

        /// <summary>
        /// Construct a <see cref="SimpleJsonFormatter"/>.
        /// </summary>
        /// <param name="omitEnclosingObject">If true, the properties of the event will be written to
        /// the output without enclosing braces. Otherwise, if false, each event will be written as a well-formed
        /// JSON object.</param>
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
                { typeof(ScalarValue), (v, w) => WriteLiteral(((ScalarValue)v).Value, w) },
                { typeof(SequenceValue), (v, w) => WriteSequence(((SequenceValue)v).Elements, w) },
                { typeof(StructureValue), (v, w) => WriteStructure(((StructureValue)v).TypeTag, ((StructureValue)v).Properties, w) },
            };
        }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (output == null) throw new ArgumentNullException("output");

            if (!_omitEnclosingObject)
                output.Write("{");

            var delim = "";
            WriteJsonProperty("TimeStamp", logEvent.TimeStamp, ref delim, output);
            WriteJsonProperty("Level", logEvent.Level, ref delim, output);
            WriteJsonProperty("MessageTemplate", logEvent.MessageTemplate, ref delim, output);

            if (logEvent.Exception != null)
                WriteJsonProperty("Exception", logEvent.Exception, ref delim, output);

            if (logEvent.Properties.Count != 0)
            {
                output.Write(",\"Properties\":{");
                var pdelim = "";
                foreach (var property in logEvent.Properties.Values)
                {
                    WriteJsonProperty(property.Name, property.Value, ref pdelim, output);
                }
                output.Write("}");
            }

            if (!_omitEnclosingObject)
                output.Write("}");
        }

        void WriteStructure(string typeTag, IEnumerable<LogEventProperty> properties, TextWriter output)
        {
            output.Write("{");

            var delim = "";
            if (typeTag != null)
                WriteJsonProperty("_typeTag", typeTag, ref delim, output);
            
            foreach (var property in properties)
                WriteJsonProperty(property.Name, property.Value, ref delim, output);

            output.Write("}");
        }

        void WriteSequence(IEnumerable elements, TextWriter output)
        {
            output.Write("[");
            foreach (var value in elements)
            {
                WriteLiteral(value, output);
                output.Write(",");
            }
            output.Write("]");
        }

        void WriteJsonProperty(string name, object value, ref string precedingDelimiter, TextWriter output)
        {
            output.Write(precedingDelimiter);
            output.Write("\"");
            output.Write(name);
            output.Write("\":");
            WriteLiteral(value, output);
            precedingDelimiter = ",";
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
