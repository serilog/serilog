// Copyright 2014 Serilog Contributors
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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Formatting.Json
{
    /// <summary>
    /// Formats log events in a simple JSON structure. Instances of this class
    /// are safe for concurrent access by multiple threads.
    /// </summary>
    public class JsonFormatter : ITextFormatter
    {
        readonly bool _omitEnclosingObject;
        readonly string _closingDelimiter;
        readonly bool _renderMessage;
        readonly IFormatProvider _formatProvider;
        readonly IDictionary<Type, Action<object, bool, TextWriter>> _literalWriters;

        private JsonFormattingStage _stage = JsonFormattingStage.MetaData;

        /// <summary>
        /// Signals to subclasses at what stage the formatter is operating
        /// </summary>
        protected JsonFormattingStage Stage
        {
            get { return _stage; }
            private set { _stage = value; }
        }
        /// <summary>
        /// Construct a <see cref="JsonFormatter"/>. Obsolete, please use named arguments
        /// when calling this constructor.
        /// </summary>
        [Obsolete("Use named arguments with this method to guarantee forwards-compatibility."), EditorBrowsable(EditorBrowsableState.Never)]
        public JsonFormatter(bool omitEnclosingObjectObsolete)
            : this(omitEnclosingObject: omitEnclosingObjectObsolete)
        {
        }

        /// <summary>
        /// Construct a <see cref="JsonFormatter"/>.
        /// </summary>
        /// <param name="omitEnclosingObject">If true, the properties of the event will be written to
        /// the output without enclosing braces. Otherwise, if false, each event will be written as a well-formed
        /// JSON object.</param>
        /// <param name="closingDelimiter">A string that will be written after each log event is formatted.
        /// If null, <see cref="Environment.NewLine"/> will be used. Ignored if <paramref name="omitEnclosingObject"/>
        /// is true.</param>
        /// <param name="renderMessage">If true, the message will be rendered and written to the output as a
        /// property named RenderedMessage.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public JsonFormatter(
            bool omitEnclosingObject = false,
            string closingDelimiter = null,
            bool renderMessage = false,
            IFormatProvider formatProvider = null)
        {
            _omitEnclosingObject = omitEnclosingObject;
            _closingDelimiter = closingDelimiter ?? Environment.NewLine;
            _renderMessage = renderMessage;
            _formatProvider = formatProvider;

            _literalWriters = new Dictionary<Type, Action<object, bool, TextWriter>>
            {
                { typeof(bool), (v, q, w) => WriteBoolean((bool)v, w) },
                { typeof(char), (v, q, w) => WriteString(((char)v).ToString(_formatProvider), w) },
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
                { typeof(string), (v, q, w) => WriteString((string)v, w) },
                { typeof(DateTime), (v, q, w) => WriteDateTime((DateTime)v, w) },
                { typeof(DateTimeOffset), (v, q, w) => WriteOffset((DateTimeOffset)v, w) },
                { typeof(ScalarValue), (v, q, w) => WriteLiteral(((ScalarValue)v).Value, w, q) },
                { typeof(SequenceValue), (v, q, w) => WriteSequence(((SequenceValue)v).Elements, w) },
                { typeof(DictionaryValue), (v, q, w) => WriteDictionary(((DictionaryValue)v).Elements, w) },
                { typeof(StructureValue), (v, q, w) => WriteStructure(((StructureValue)v).TypeTag, ((StructureValue)v).Properties, w) },
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
            WriteJsonProperty("Timestamp", logEvent.Timestamp, ref delim, output);
            WriteJsonProperty("Level", logEvent.Level, ref delim, output);
            WriteJsonProperty("MessageTemplate", logEvent.MessageTemplate.Text, ref delim, output);
            if (_renderMessage)
                WriteJsonProperty("RenderedMessage", logEvent.RenderMessage(_formatProvider), ref delim, output);

            if (logEvent.Exception != null)
                WriteJsonProperty("Exception", logEvent.Exception, ref delim, output);

            if (logEvent.Properties.Count != 0)
            {
                output.Write(",\"{0}\":{{", FormatPropertyName("Properties"));
                Stage = JsonFormattingStage.Properties;
                var precedingDelimiter = "";
                foreach (var property in logEvent.Properties)
                {
                    WriteJsonProperty(property.Key, property.Value, ref precedingDelimiter, output);
                }
                output.Write("}");
                Stage = JsonFormattingStage.MetaData;
            }

            var tokensWithFormat = logEvent.MessageTemplate.Tokens
                .OfType<PropertyToken>()
                .Where(pt => pt.Format != null)
                .GroupBy(pt => pt.PropertyName)
                .ToArray();

            if (tokensWithFormat.Length != 0)
            {
                output.Write(",\"{0}\":{{", FormatPropertyName("Renderings"));
                Stage = JsonFormattingStage.Renderings;
                var rdelim = "";
                foreach (var ptoken in tokensWithFormat)
                {
                    output.Write(rdelim);
                    rdelim = ",";
                    output.Write("\"");
                    output.Write(ptoken.Key);
                    output.Write("\":[");

                    var fdelim = "";
                    foreach (var format in ptoken)
                    {
                        output.Write(fdelim);
                        fdelim = ",";

                        output.Write("{");
                        var eldelim = "";

                        WriteJsonProperty("Format", format.Format, ref eldelim, output);

                        var sw = new StringWriter();
                        format.Render(logEvent.Properties, sw);
                        WriteJsonProperty("Rendering", sw.ToString(), ref eldelim, output);

                        output.Write("}");
                    }

                    output.Write("]");
                }
                output.Write("}");
                Stage = JsonFormattingStage.MetaData;
            }

            if (!_omitEnclosingObject)
            {
                output.Write("}");
                output.Write(_closingDelimiter);
            }
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
            var delim = "";
            foreach (var value in elements)
            {
                output.Write(delim);
                delim = ",";
                WriteLiteral(value, output);
            }
            output.Write("]");
        }

        void WriteDictionary(IDictionary<ScalarValue, LogEventPropertyValue> elements, TextWriter output)
        {
            output.Write("{");
            var delim = "";
            foreach (var e in elements)
            {
                output.Write(delim);
                delim = ",";
                WriteLiteral(e.Key, output, true);
                output.Write(":");
                WriteLiteral(e.Value, output);
            }
            output.Write("}");
        }

        void WriteJsonProperty(string name, object value, ref string precedingDelimiter, TextWriter output)
        {
            output.Write(precedingDelimiter);
            output.Write("\"");
            output.Write(FormatPropertyName(name));
            output.Write("\":");
            WriteLiteral(value, output);
            precedingDelimiter = ",";
        }

        /// <summary>
        /// Allows you subclass JsonFormatter and implement your own property name handling
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string FormatPropertyName(string name)
        {
            return name;
        }

        /// <summary>
        /// Allows a subclass to write out objects that have no configured literal writer.
        /// </summary>
        /// <param name="value">The value to be written as a json construct</param>
        /// <param name="output">The writer to write on</param>
        protected virtual void WriteObjectValue(object value, TextWriter output)
        {
            WriteString(value.ToString(), output);
        }

        void WriteLiteral(object value, TextWriter output, bool forceQuotation = false)
        {
            if (value == null)
            {
                output.Write("null");
                return;
            }

            Action<object, bool, TextWriter> writer;
            if (_literalWriters.TryGetValue(value.GetType(), out writer))
            {
                writer(value, forceQuotation, output);
                return;
            }

            WriteObjectValue(value, output);
        }

        static void WriteToString(object number, bool quote, TextWriter output)
        {
            if (quote) output.Write('"');

            var fmt = number as IFormattable;
            if (fmt != null)
                output.Write(fmt.ToString(null, CultureInfo.InvariantCulture));
            else
                output.Write(number.ToString());

            if (quote) output.Write('"');
        }

        static void WriteBoolean(bool value, TextWriter output)
        {
            output.Write(value ? "true" : "false");
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
            var content = Escape(value);
            output.Write("\"");
            output.Write(content);
            output.Write("\"");
        }

        /// <summary>
        /// Perform simple JSON string escaping on <paramref name="s"/>.
        /// </summary>
        /// <param name="s">A raw string.</param>
        /// <returns>A JSON-escaped version of <paramref name="s"/>.</returns>
        public static string Escape(string s)
        {
            if (s == null) return null;

            StringBuilder escapedResult = null;
            var cleanSegmentStart = 0;
            for (var i = 0; i < s.Length; ++i)
            {
                var c = s[i];
                if (c < (char)32 || c == '\\' || c == '"')
                {

                    if (escapedResult == null)
                        escapedResult = new StringBuilder();

                    escapedResult.Append(s.Substring(cleanSegmentStart, i - cleanSegmentStart));
                    cleanSegmentStart = i + 1;

                    switch (c)
                    {
                        case '"':
                            {
                                escapedResult.Append("\\\"");
                                break;
                            }
                        case '\\':
                            {
                                escapedResult.Append("\\\\");
                                break;
                            }
                        case '\n':
                            {
                                escapedResult.Append("\\n");
                                break;
                            }
                        case '\r':
                            {
                                escapedResult.Append("\\r");
                                break;
                            }
                        case '\f':
                            {
                                escapedResult.Append("\\f");
                                break;
                            }
                        case '\t':
                            {
                                escapedResult.Append("\\t");
                                break;
                            }
                        default:
                            {
                                escapedResult.Append("\\u");
                                escapedResult.Append(((int)c).ToString("X4"));
                                break;
                            }
                    }
                }
            }

            if (escapedResult != null)
            {
                if (cleanSegmentStart != s.Length)
                    escapedResult.Append(s.Substring(cleanSegmentStart));

                return escapedResult.ToString();
            }

            return s;
        }
    }
}
