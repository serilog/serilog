// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Formatting.Json;

/// <summary>
/// Formats log events in a simple JSON structure. Instances of this class
/// are safe for concurrent access by multiple threads.
/// </summary>
public class JsonFormatter : ITextFormatter
{
    // Ignore obsoletion errors
#pragma warning disable 618

    readonly string _closingDelimiter;
    readonly bool _renderMessage;
    readonly IFormatProvider? _formatProvider;
    readonly IDictionary<Type, Action<object, bool, TextWriter>> _literalWriters;

    /// <summary>
    /// Construct a <see cref="JsonFormatter"/>.
    /// </summary>
    /// <param name="closingDelimiter">A string that will be written after each log event is formatted.
    /// If null, <see cref="Environment.NewLine"/> will be used.</param>
    /// <param name="renderMessage">If true, the message will be rendered and written to the output as a
    /// property named RenderedMessage.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    public JsonFormatter(
        string? closingDelimiter = null,
        bool renderMessage = false,
        IFormatProvider? formatProvider = null)
    {
        _closingDelimiter = closingDelimiter ?? Environment.NewLine;
        _renderMessage = renderMessage;
        _formatProvider = formatProvider;

        _literalWriters = new Dictionary<Type, Action<object, bool, TextWriter>>
        {
            { typeof(bool), (v, _, w) => WriteBoolean((bool)v, w) },
            { typeof(char), (v, _, w) => WriteString(((char)v).ToString(), w) },
            { typeof(byte), WriteToString },
            { typeof(sbyte), WriteToString },
            { typeof(short), WriteToString },
            { typeof(ushort), WriteToString },
            { typeof(int), WriteToString },
            { typeof(uint), WriteToString },
            { typeof(long), WriteToString },
            { typeof(ulong), WriteToString },
            { typeof(float), (v, _, w) => WriteSingle((float)v, w) },
            { typeof(double), (v, _, w) => WriteDouble((double)v, w) },
            { typeof(decimal), WriteToString },
            { typeof(string), (v, _, w) => WriteString((string)v, w) },
            { typeof(DateTime), (v, _, w) => WriteDateTime((DateTime)v, w) },
            { typeof(DateTimeOffset), (v, _, w) => WriteOffset((DateTimeOffset)v, w) },
#if FEATURE_DATE_AND_TIME_ONLY
            { typeof(DateOnly), (v, _, w) => WriteDateOnly((DateOnly)v, w) },
            { typeof(TimeOnly), (v, _, w) => WriteTimeOnly((TimeOnly)v, w) },
#endif
            { typeof(ScalarValue), (v, q, w) => WriteLiteral(((ScalarValue)v).Value, w, q) },
            { typeof(SequenceValue), (v, _, w) => WriteSequence(((SequenceValue)v).Elements, w) },
            { typeof(DictionaryValue), (v, _, w) => WriteDictionary(((DictionaryValue)v).Elements, w) },
            { typeof(StructureValue), (v, _, w) => WriteStructure(((StructureValue)v).TypeTag, ((StructureValue)v).Properties, w) },
        };
    }

    /// <summary>
    /// Format the log event into the output.
    /// </summary>
    /// <param name="logEvent">The event to format.</param>
    /// <param name="output">The output.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="logEvent"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="output"/> is <code>null</code></exception>
    public void Format(LogEvent logEvent, TextWriter output)
    {
        Guard.AgainstNull(logEvent);
        Guard.AgainstNull(output);

        output.Write('{');

        var delim = "";
        WriteTimestamp(logEvent.Timestamp, ref delim, output);
        WriteLevel(logEvent.Level, ref delim, output);
        WriteMessageTemplate(logEvent.MessageTemplate.Text, ref delim, output);
        if (_renderMessage)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            WriteRenderedMessage(message, ref delim, output);
        }

        if (logEvent.Exception != null)
            WriteException(logEvent.Exception, ref delim, output);

        if (logEvent.Properties.Count != 0)
            WriteProperties(logEvent.Properties, output);

        var tokensWithFormat = logEvent.MessageTemplate.TokenArray
            .OfType<PropertyToken>()
            .Where(pt => pt.Format != null)
            .GroupBy(pt => pt.PropertyName)
            .ToArray();

        if (tokensWithFormat.Length != 0)
        {
            WriteRenderings(tokensWithFormat, logEvent.Properties, output);
        }

        output.Write('}');
        output.Write(_closingDelimiter);
    }

    /// <summary>
    /// Adds a writer function for a given type.
    /// </summary>
    /// <param name="type">The type of values, which <paramref name="writer" /> handles.</param>
    /// <param name="writer">The function, which writes the values.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="type"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="writer"/> is <code>null</code></exception>
    void AddLiteralWriter(Type type, Action<object, TextWriter> writer)
    {
        Guard.AgainstNull(type);
        Guard.AgainstNull(writer);

        _literalWriters[type] = (v, _, w) => writer(v, w);
    }

    /// <summary>
    /// Writes out individual renderings of attached properties
    /// </summary>
    void WriteRenderings(IGrouping<string, PropertyToken>[] tokensWithFormat, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        output.Write(",\"{0}\":{{", "Renderings");
        WriteRenderingsValues(tokensWithFormat, properties, output);
        output.Write('}');
    }

    /// <summary>
    /// Writes out the values of individual renderings of attached properties
    /// </summary>
    void WriteRenderingsValues(IGrouping<string, PropertyToken>[] tokensWithFormat, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        var propertyDelimiter = "";
        foreach (var propertyFormats in tokensWithFormat)
        {
            output.Write(propertyDelimiter);
            propertyDelimiter = ",";
            output.Write('"');
            output.Write(propertyFormats.Key);
            output.Write("\":[");

            var formatDelimiter = "";
            foreach (var format in propertyFormats)
            {
                output.Write(formatDelimiter);
                formatDelimiter = ",";

                output.Write('{');
                var elementDelimiter = "";

                WriteJsonProperty("Format", format.Format, ref elementDelimiter, output);

                var sw = new StringWriter();
                MessageTemplateRenderer.RenderPropertyToken(format, properties, sw, _formatProvider, isLiteral: true, isJson: false);
                WriteJsonProperty("Rendering", sw.ToString(), ref elementDelimiter, output);

                output.Write('}');
            }

            output.Write(']');
        }
    }

    /// <summary>
    /// Writes out the attached properties
    /// </summary>
    void WriteProperties(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        output.Write(",\"{0}\":{{", "Properties");
        WritePropertiesValues(properties, output);
        output.Write('}');
    }

    /// <summary>
    /// Writes out the attached properties values
    /// </summary>
    void WritePropertiesValues(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        var precedingDelimiter = "";
        foreach (var property in properties)
        {
            WriteJsonProperty(property.Key, property.Value, ref precedingDelimiter, output);
        }
    }

    /// <summary>
    /// Writes out the attached exception
    /// </summary>
    void WriteException(Exception exception, ref string delim, TextWriter output)
    {
        WriteJsonProperty("Exception", exception, ref delim, output);
    }

    /// <summary>
    /// (Optionally) writes out the rendered message
    /// </summary>
    void WriteRenderedMessage(string message, ref string delim, TextWriter output)
    {
        WriteJsonProperty("RenderedMessage", message, ref delim, output);
    }

    /// <summary>
    /// Writes out the message template for the logevent.
    /// </summary>
    void WriteMessageTemplate(string template, ref string delim, TextWriter output)
    {
        WriteJsonProperty("MessageTemplate", template, ref delim, output);
    }

    /// <summary>
    /// Writes out the log level
    /// </summary>
    void WriteLevel(LogEventLevel level, ref string delim, TextWriter output)
    {
        WriteJsonProperty("Level", level, ref delim, output);
    }

    /// <summary>
    /// Writes out the log timestamp
    /// </summary>
    void WriteTimestamp(DateTimeOffset timestamp, ref string delim, TextWriter output)
    {
        WriteJsonProperty("Timestamp", timestamp, ref delim, output);
    }

    /// <summary>
    /// Writes out a structure property
    /// </summary>
    void WriteStructure(string? typeTag, IEnumerable<LogEventProperty> properties, TextWriter output)
    {
        output.Write('{');

        var delim = "";
        if (typeTag != null)
            WriteJsonProperty("_typeTag", typeTag, ref delim, output);

        foreach (var property in properties)
            WriteJsonProperty(property.Name, property.Value, ref delim, output);

        output.Write('}');
    }

    /// <summary>
    /// Writes out a sequence property
    /// </summary>
    void WriteSequence(IEnumerable elements, TextWriter output)
    {
        output.Write('[');
        var delim = "";
        foreach (var value in elements)
        {
            output.Write(delim);
            delim = ",";
            WriteLiteral(value, output);
        }
        output.Write(']');
    }

    /// <summary>
    /// Writes out a dictionary
    /// </summary>
    void WriteDictionary(IReadOnlyDictionary<ScalarValue, LogEventPropertyValue> elements, TextWriter output)
    {
        output.Write('{');
        var delim = "";
        foreach (var element in elements)
        {
            output.Write(delim);
            delim = ",";
            WriteLiteral(element.Key, output, forceQuotation: true);
            output.Write(':');
            WriteLiteral(element.Value, output);
        }
        output.Write('}');
    }

    /// <summary>
    /// Writes out a json property with the specified value on output writer
    /// </summary>
    void WriteJsonProperty(string name, object? value, ref string precedingDelimiter, TextWriter output)
    {
        output.Write(precedingDelimiter);
        output.Write('"');
        output.Write(name);
        output.Write("\":");
        WriteLiteral(value, output);
        precedingDelimiter = ",";
    }

    /// <summary>
    /// Allows a subclass to write out objects that have no configured literal writer.
    /// </summary>
    /// <param name="value">The value to be written as a json construct</param>
    /// <param name="output">The writer to write on</param>
    static void WriteLiteralValue(object value, TextWriter output)
    {
        WriteString(value.ToString() ?? "", output);
    }

    void WriteLiteral(object? value, TextWriter output, bool forceQuotation = false)
    {
        if (value == null)
        {
            output.Write("null");
            return;
        }

        if (_literalWriters.TryGetValue(value.GetType(), out var writer))
        {
            writer(value, forceQuotation, output);
            return;
        }

        WriteLiteralValue(value, output);
    }

    static void WriteToString(object number, bool quote, TextWriter output)
    {
        if (quote) output.Write('"');

        if (number is IFormattable fmt)
            output.Write(fmt.ToString(null, CultureInfo.InvariantCulture));
        else
            output.Write(number.ToString());

        if (quote) output.Write('"');
    }

    static void WriteBoolean(bool value, TextWriter output)
    {
        output.Write(value ? "true" : "false");
    }

    static void WriteSingle(float value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out int written, format: "R", CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#endif
    }

    static void WriteDouble(double value, TextWriter output)
    {
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out int written, format: "R", CultureInfo.InvariantCulture))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#else
        output.Write(value.ToString("R", CultureInfo.InvariantCulture));
#endif
    }

    static void WriteOffset(DateTimeOffset value, TextWriter output)
    {
        output.Write('"');
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out int written, format: "o"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("o"));
#else
        output.Write(value.ToString("o"));
#endif
        output.Write('"');
    }

    static void WriteDateTime(DateTime value, TextWriter output)
    {
        output.Write('"');
#if FEATURE_SPAN
        Span<char> buffer = stackalloc char[64];
        if (value.TryFormat(buffer, out int written, format: "o"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("o"));
#else
        output.Write(value.ToString("o"));
#endif
        output.Write('"');
    }

#if FEATURE_DATE_AND_TIME_ONLY

    static void WriteDateOnly(DateOnly value, TextWriter output)
    {
        output.Write('"');
        Span<char> buffer = stackalloc char[10];
        if (value.TryFormat(buffer, out int written, format: "yyyy-MM-dd"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("yyyy-MM-dd"));

        output.Write('"');
    }

    static void WriteTimeOnly(TimeOnly value, TextWriter output)
    {
        output.Write('"');

        Span<char> buffer = stackalloc char[16];
        if (value.TryFormat(buffer, out int written, format: "O"))
            output.Write(buffer.Slice(0, written));
        else
            output.Write(value.ToString("O"));

        output.Write('"');
    }

#endif

    static void WriteString(string value, TextWriter output)
    {
        JsonValueFormatter.WriteQuotedJsonString(value, output);
    }
}
