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
/// <remarks>New code should prefer formatters from <c>Serilog.Formatting.Compact</c>, or <c>ExpressionTemplate</c> from
/// <c>Serilog.Expressions</c>.</remarks>
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

        output.Write("{\"Timestamp\":\"");
        output.Write(logEvent.Timestamp.ToString("O"));
        output.Write("\",\"Level\":\"");
        output.Write(logEvent.Level);
        output.Write("\",\"MessageTemplate\":");
        JsonValueFormatter.WriteQuotedJsonString(logEvent.MessageTemplate.Text, output);

        if (_renderMessage)
        {
            output.Write("\",\"RenderedMessage\":");
            var message = logEvent.MessageTemplate.Render(logEvent.Properties);
            JsonValueFormatter.WriteQuotedJsonString(message, output);
        }

        if (logEvent.Exception != null)
        {
            output.Write(",\"Exception\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
        }

        if (logEvent.Properties.Count != 0)
        {
            output.Write(",\"Properties\":{");
            foreach (var property in logEvent.Properties)
            {
                output.Write(',');
                JsonValueFormatter.WriteQuotedJsonString(property.Key, output);
                output.Write(':');
                _jsonValueFormatter.Format(property.Value, output);
            }

            output.Write('}');
        }

        var tokensWithFormat = logEvent.MessageTemplate.TokenArray
            .OfType<PropertyToken>()
            .Where(pt => pt.Format != null)
            .GroupBy(pt => pt.PropertyName)
            .ToArray();

        if (tokensWithFormat.Length != 0)
        {
            output.Write(",\"Renderings\":{");
            WriteRenderingsValues(tokensWithFormat, logEvent.Properties, output);
            output.Write('}');
        }

        output.Write('}');
        output.Write(_closingDelimiter);
    }

    void WriteRenderingsValues(IEnumerable<IGrouping<string, PropertyToken>> tokensWithFormat, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        static void WriteNameValuePair(string name, string value, ref char? precedingDelimiter, TextWriter output)
        {
            if (precedingDelimiter != null)
                output.Write(precedingDelimiter.Value);

            JsonValueFormatter.WriteQuotedJsonString(name, output);
            output.Write(':');
            JsonValueFormatter.WriteQuotedJsonString(value, output);
            precedingDelimiter = ',';
        }

        char? propertyDelimiter = null;
        foreach (var propertyFormats in tokensWithFormat)
        {
            if (propertyDelimiter != null)
                output.Write(propertyDelimiter.Value);

            propertyDelimiter = ',';
            output.Write('"');
            output.Write(propertyFormats.Key);
            output.Write("\":[");

            char? formatDelimiter = null;
            foreach (var format in propertyFormats)
            {
                if (formatDelimiter != null)
                    output.Write(formatDelimiter.Value);

                formatDelimiter = ',';

                output.Write('{');
                char? elementDelimiter = null;

                // Caller ensures that `tokensWithFormat` contains only property tokens that have non-null `Format`s.
                WriteNameValuePair("Format", format.Format!, ref elementDelimiter, output);

                using var sw = ReusableStringWriter.GetOrCreate();
                MessageTemplateRenderer.RenderPropertyToken(format, properties, sw, _formatProvider, isLiteral: true, isJson: false);
                WriteNameValuePair("Rendering", sw.ToString(), ref elementDelimiter, output);

                output.Write('}');
            }

            output.Write(']');
        }
    }
}
