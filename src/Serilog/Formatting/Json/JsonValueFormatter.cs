// Copyright 2016 Serilog Contributors
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
using System.Globalization;
using System.IO;
using Serilog.Data;
using Serilog.Events;

namespace Serilog.Formatting.Json
{
    /// <summary>
    /// Converts Serilog's structured property value format into JSON.
    /// </summary>
    public class JsonValueFormatter : LogEventPropertyValueVisitor<TextWriter, bool>
    {
        readonly string _typeTagName;

        const string DefaultTypeTagName = "_typeTag";

        /// <summary>
        /// Construct a <see cref="JsonFormatter"/>.
        /// </summary>
        /// <param name="typeTagName">When serializing structured (object) values,
        /// the property name to use for the Serilog <see cref="StructureValue.TypeTag"/> field
        /// in the resulting JSON. If null, no type tag field will be written. The default is
        /// "_typeTag".</param>
        public JsonValueFormatter(string typeTagName = DefaultTypeTagName)
        {
            _typeTagName = typeTagName;
        }

        /// <summary>
        /// Format <paramref name="value"/> as JSON to <paramref name="output"/>.
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <param name="output">The output</param>
        public void Format(LogEventPropertyValue value, TextWriter output)
        {
            // Parameter order of ITextFormatter is the reverse of the visitor one.
            // In this class, public methods and methods with Format*() names use the
            // (x, output) parameter naming convention.
            Visit(output, value);
        }

        /// <summary>
        /// Visit a <see cref="ScalarValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="scalar">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="scalar"/>.</returns>
        protected override bool VisitScalarValue(TextWriter state, ScalarValue scalar)
        {
            if (scalar == null) throw new ArgumentNullException(nameof(scalar));
            FormatLiteralValue(scalar.Value, state);
            return false;
        }

        /// <summary>
        /// Visit a <see cref="SequenceValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="sequence">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="sequence"/>.</returns>
        protected override bool VisitSequenceValue(TextWriter state, SequenceValue sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            state.Write('[');
            var delim = "";
            for (var i = 0; i < sequence.Elements.Count; i++)
            {
                state.Write(delim);
                delim = ",";
                Visit(state, sequence.Elements[i]);
            }
            state.Write(']');
            return false;
        }

        /// <summary>
        /// Visit a <see cref="StructureValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="structure">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="structure"/>.</returns>
        protected override bool VisitStructureValue(TextWriter state, StructureValue structure)
        {
            state.Write('{');

            var delim = "";

            for (var i = 0; i < structure.Properties.Count; i++)
            {
                state.Write(delim);
                delim = ",";
                var prop = structure.Properties[i];
                WriteQuotedJsonString(prop.Name, state);
                state.Write(':');
                Visit(state, prop.Value);
            }

            if (_typeTagName != null && structure.TypeTag != null)
            {
                state.Write(delim);
                WriteQuotedJsonString(_typeTagName, state);
                state.Write(':');
                WriteQuotedJsonString(structure.TypeTag, state);
            }

            state.Write('}');
            return false;
        }

        /// <summary>
        /// Visit a <see cref="DictionaryValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="dictionary">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="dictionary"/>.</returns>
        protected override bool VisitDictionaryValue(TextWriter state, DictionaryValue dictionary)
        {
            state.Write('{');
            var delim = "";
            foreach (var element in dictionary.Elements)
            {
                state.Write(delim);
                delim = ",";
                WriteQuotedJsonString((element.Key.Value ?? "null").ToString(), state);
                state.Write(':');
                Visit(state, element.Value);
            }
            state.Write('}');
            return false;
        }

        /// <summary>
        /// Write a literal as a single JSON value, e.g. as a number or string. Override to
        /// support more value types. Don't write arrays/structures through this method - the
        /// active destructuring policies have already indicated the value should be scalar at
        /// this point.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="output">The output</param>
        protected virtual void FormatLiteralValue(object value, TextWriter output)
        {
            if (value == null)
            {
                FormatNullValue(output);
                return;
            }

            // Although the linear switch-on-type has apparently worse algorithmic performance than the O(1)
            // dictionary lookup alternative, in practice, it's much to make a few equality comparisons
            // than the hash/bucket dictionary lookup, and since most data will be string (one comparison),
            // numeric (a handful) or an object (two comparsions) the real-world performance of the code
            // as written is as fast or faster.

            var str = value as string;
            if (str != null)
            {
                FormatStringValue(str, output);
                return;
            }

            if (value is ValueType)
            {
                if (value is int || value is uint || value is long || value is ulong || value is decimal ||
                    value is byte || value is sbyte || value is short || value is ushort)
                {
                    FormatExactNumericValue((IFormattable)value, output);
                    return;
                }

				if (value is double)
				{
					FormatDoubleValue((double)value, output);
					return;
				}

				if (value is float)
				{
					FormatFloatValue((float)value, output);
					return;
				}

                if (value is bool)
                {
                    FormatBooleanValue((bool)value, output);
                    return;
                }

                if (value is char)
                {
                    FormatStringValue(value.ToString(), output);
                    return;
                }

                if (value is DateTime || value is DateTimeOffset)
                {
                    FormatDateTimeValue((IFormattable)value, output);
                    return;
                }

                if (value is TimeSpan)
                {
                    FormatTimeSpanValue((TimeSpan)value, output);
                    return;
                }
            }

            FormatLiteralObjectValue(value, output);
        }

        static void FormatBooleanValue(bool value, TextWriter output)
        {
            output.Write(value ? "true" : "false");
        }

		static void FormatFloatValue(float value, TextWriter output)
		{
			if (float.IsNaN(value) || float.IsInfinity(value))
			{
				FormatStringValue(value.ToString(CultureInfo.InvariantCulture), output);
				return;
			}

			output.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		static void FormatDoubleValue(double value, TextWriter output)
		{
			if (double.IsNaN(value) || double.IsInfinity(value))
			{
				FormatStringValue(value.ToString(CultureInfo.InvariantCulture), output);
				return;
			}

			output.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

        static void FormatExactNumericValue(IFormattable value, TextWriter output)
        {
            output.Write(value.ToString(null, CultureInfo.InvariantCulture));
        }

        static void FormatDateTimeValue(IFormattable value, TextWriter output)
        {
            output.Write('\"');
            output.Write(value.ToString("O", CultureInfo.InvariantCulture));
            output.Write('\"');
        }

        static void FormatTimeSpanValue(TimeSpan value, TextWriter output)
        {
            output.Write('\"');
            output.Write(value.ToString());
            output.Write('\"');
        }

        static void FormatLiteralObjectValue(object value, TextWriter output)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            FormatStringValue(value.ToString(), output);
        }

        static void FormatStringValue(string str, TextWriter output)
        {
            WriteQuotedJsonString(str, output);
        }

        static void FormatNullValue(TextWriter output)
        {
            output.Write("null");
        }

        /// <summary>
        /// Write a valid JSON string literal, escaping as necessary.
        /// </summary>
        /// <param name="str">The string value to write.</param>
        /// <param name="output">The output.</param>
        public static void WriteQuotedJsonString(string str, TextWriter output)
        {
            output.Write('\"');

            var cleanSegmentStart = 0;
            var anyEscaped = false;

            for (var i = 0; i < str.Length; ++i)
            {
                var c = str[i];
                if (c < (char)32 || c == '\\' || c == '"')
                {
                    anyEscaped = true;

                    output.Write(str.Substring(cleanSegmentStart, i - cleanSegmentStart));
                    cleanSegmentStart = i + 1;

                    switch (c)
                    {
                        case '"':
                            {
                                output.Write("\\\"");
                                break;
                            }
                        case '\\':
                            {
                                output.Write("\\\\");
                                break;
                            }
                        case '\n':
                            {
                                output.Write("\\n");
                                break;
                            }
                        case '\r':
                            {
                                output.Write("\\r");
                                break;
                            }
                        case '\f':
                            {
                                output.Write("\\f");
                                break;
                            }
                        case '\t':
                            {
                                output.Write("\\t");
                                break;
                            }
                        default:
                            {
                                output.Write("\\u");
                                output.Write(((int)c).ToString("X4"));
                                break;
                            }
                    }
                }
            }

            if (anyEscaped)
            {
                if (cleanSegmentStart != str.Length)
                    output.Write(str.Substring(cleanSegmentStart));
            }
            else
            {
                output.Write(str);
            }

            output.Write('\"');
        }
    }
}
