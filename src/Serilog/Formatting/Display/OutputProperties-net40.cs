// Copyright 2013 Serilog Contributors
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
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    /// <summary>
    /// Describes the properties available in standard message template-based
    /// output format strings.
    /// </summary>
    public static class OutputProperties
    {
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
        /// Message which will be rendered in place of an exception if the original exception could not be rendered
        /// </summary>
        public const string FailedToRenderExceptionMessage = "-Could Not Render Exception-";

        /// <summary>
        /// Create properties from the provided log event.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <returns>A dictionary with properties representing the log event.</returns>
        public static IDictionary<string, LogEventPropertyValue> GetOutputProperties(LogEvent logEvent)
        {
            var result = logEvent.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // "Special" output properties like Message will override any properties with the same name
            // when used in format strings; this doesn't affect the rendering of the message template,
            // which uses only the log event properties.

            result[MessagePropertyName] = new LogEventPropertyMessageValue(logEvent.MessageTemplate, logEvent.Properties);
            result[TimestampPropertyName] = new ScalarValue(logEvent.Timestamp);
            result[LevelPropertyName] = new ScalarValue(logEvent.Level);
            result[NewLinePropertyName] = new LiteralStringValue(Environment.NewLine);

            var exception = RenderException(logEvent.Exception);
            result[ExceptionPropertyName] = new LiteralStringValue(exception);

            return result;
        }
        
        /// <summary>
        /// Renders an exception to a string
        /// </summary>
        /// <param name="exception">Exception to render</param>
        private static string RenderException(Exception exception)
        {
            if (exception != null)
            {
                // Handle exception which themselves throw exceptions when rendered - don't prevent
                // the log message from being written!
                try
                {
                    return exception + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Failed to Render Exception via ToString(): " + ex);

                    // .ToString() on the exception threw - render a fallback representation
                    try
                    {
                        return String.Format("Exception {0}: {1}", exception.GetType().FullName, exception.Message) 
                            + Environment.NewLine;
                    }
                    catch (Exception)
                    {
                        // .Message on the exception also threw, just render a static complaint
                        return FailedToRenderExceptionMessage + Environment.NewLine;
                    }
                }
            }

            return String.Empty;
        }
    }
}
