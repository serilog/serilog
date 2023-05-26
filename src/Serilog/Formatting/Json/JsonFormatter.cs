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
public sealed class JsonFormatter : ITextFormatter
{
    readonly string _closingDelimiter;
    readonly bool _renderMessage;
    readonly IFormatProvider? _formatProvider;
    readonly JsonValueFormatter _jsonValueFormatter = new();

    /// <summary>
    /// Construct a <see cref="JsonFormatter"/>.
    /// </summary>
    /// <param name="closingDelimiter">A string that will be written after each log event is formatted.
    /// If null, <see cref="Environment.NewLine"/> will be used.</param>
    /// <param name="renderMessage">If <see langword="true"/>, the message will be rendered and written to the output as a
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

        char? delim = null;
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
    /// Writes out individual renderings of attached properties
    /// </summary>
    void WriteRenderings(IGrouping<string, PropertyToken>[] tokensWithFormat, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        output.Write(",\"Renderings\":{");
        WriteRenderingsValues(tokensWithFormat, properties, output);
        output.Write('}');
    }

    /// <summary>
    /// Writes out the values of individual renderings of attached properties
    /// </summary>
    void WriteRenderingsValues(IGrouping<string, PropertyToken>[] tokensWithFormat, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        char? propertyDelimiter = null;
        foreach (var propertyFormats in tokensWithFormat)
        {
            if (propertyDelimiter != null)
            {
                output.Write(propertyDelimiter.Value);
            }

            propertyDelimiter = ',';
            output.Write('"');
            output.Write(propertyFormats.Key);
            output.Write("\":[");

            char? formatDelimiter = null;
            foreach (var format in propertyFormats)
            {
                if (formatDelimiter != null)
                {
                    output.Write(formatDelimiter.Value);
                }
                formatDelimiter = ',';

                output.Write('{');
                char? elementDelimiter = null;

                WriteJsonProperty("Format", format.Format, ref elementDelimiter, output);

                using var sw = ReusableStringWriter.GetOrCreate();
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
        output.Write(",\"Properties\":{");
        char? precedingDelimiter = null;
        foreach (var property in properties)
        {
            if (precedingDelimiter != null)
            {
                output.Write(precedingDelimiter.Value);
            }

            output.Write('"');
            output.Write(property.Key);
            output.Write("\":");
            _jsonValueFormatter.Format(property.Value, output);
            precedingDelimiter = ',';
        }
        output.Write('}');
    }


    /// <summary>
    /// Writes out the attached exception
    /// </summary>
    void WriteException(Exception exception, ref char? delim, TextWriter output)
    {
        WriteJsonProperty("Exception", exception, ref delim, output);
    }

    /// <summary>
    /// (Optionally) writes out the rendered message
    /// </summary>
    void WriteRenderedMessage(string message, ref char? delim, TextWriter output)
    {
        WriteJsonProperty("RenderedMessage", message, ref delim, output);
    }

    /// <summary>
    /// Writes out the message template for the logevent.
    /// </summary>
    void WriteMessageTemplate(string template, ref char? delim, TextWriter output)
    {
        WriteJsonProperty("MessageTemplate", template, ref delim, output);
    }

    /// <summary>
    /// Writes out the log level
    /// </summary>
    void WriteLevel(LogEventLevel level, ref char? delim, TextWriter output)
    {
        WriteJsonProperty("Level", level, ref delim, output);
    }

    /// <summary>
    /// Writes out the log timestamp
    /// </summary>
    void WriteTimestamp(DateTimeOffset timestamp, ref char? delim, TextWriter output)
    {
        WriteJsonProperty("Timestamp", timestamp, ref delim, output);
    }

    /// <summary>
    /// Writes out a json property with the specified value on output writer
    /// </summary>
    void WriteJsonProperty(string name, object? value, ref char? precedingDelimiter, TextWriter output)
    {
        if (precedingDelimiter != null)
        {
            output.Write(precedingDelimiter.Value);
        }

        output.Write('"');
        output.Write(name);
        output.Write("\":");
        _jsonValueFormatter.Format(new ScalarValue(value), output);
        precedingDelimiter = ',';
    }
}
