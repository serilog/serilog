// Copyright 2013-2017 Serilog Contributors
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
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;
using Serilog.Formatting.Display.Obsolete;

#pragma warning disable 618

namespace Serilog.Formatting.Display
{
    /// <summary>
    /// Describes the properties available in standard message template-based
    /// output format strings.
    /// </summary>
    public static class OutputProperties
    {
        static readonly LiteralStringValue LiteralNewLine = new LiteralStringValue(Environment.NewLine);

        /// <summary>
        /// The message rendered from the log event.
        /// </summary>
        public const string MessagePropertyName = "Message";

        /// <summary>
        /// The timestamp of the log event.
        /// </summary>
        public const string TimestampPropertyName = "Timestamp";

        /// <summary>
        /// The level of the log event.
        /// </summary>
        public const string LevelPropertyName = "Level";

        /// <summary>
        /// A new line.
        /// </summary>
        public const string NewLinePropertyName = "NewLine";

        /// <summary>
        /// The exception associated with the log event.
        /// </summary>
        public const string ExceptionPropertyName = "Exception";

        /// <summary>
        /// The properties of the log event.
        /// </summary>
        public const string PropertiesPropertyName = "Properties";

        /// <summary>
        /// Create properties from the provided log event.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <returns>A dictionary with properties representing the log event.</returns>
        [Obsolete("These implementation details of output formatting will not be exposed in a future version.")]
        public static IReadOnlyDictionary<string, LogEventPropertyValue> GetOutputProperties(LogEvent logEvent)
        {
            return GetOutputProperties(logEvent, MessageTemplate.Empty);
        }

        /// <summary>
        /// Create properties from the provided log event.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <param name="outputTemplate">The output template.</param>
        /// <returns>A dictionary with properties representing the log event.</returns>
        internal static IReadOnlyDictionary<string, LogEventPropertyValue> GetOutputProperties(LogEvent logEvent, MessageTemplate outputTemplate)
        {
            var result = logEvent.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // "Special" output properties like Message will override any properties with the same name
            // when used in format strings; this doesn't affect the rendering of the message template,
            // which uses only the log event properties.

            result[MessagePropertyName] = new LogEventPropertyMessageValue(logEvent.MessageTemplate, logEvent.Properties);
            result[TimestampPropertyName] = new ScalarValue(logEvent.Timestamp);
            result[LevelPropertyName] = new LogEventLevelValue(logEvent.Level);
            result[NewLinePropertyName] = LiteralNewLine;
            result[PropertiesPropertyName] = new LogEventPropertiesValue(logEvent.MessageTemplate, logEvent.Properties, outputTemplate);

            var exception = logEvent.Exception == null ? "" : logEvent.Exception + Environment.NewLine;
            result[ExceptionPropertyName] = new LiteralStringValue(exception);

            return result;
        }
    }
}
