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

using System;
using System.Collections.Generic;
using System.IO;

using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Formatting.Display
{
    // allows for the specific handling of the {Level} element.
    // can now have a fixed width applied to it, as well as casing rules.
    class LogEventLevelValue : LogEventPropertyValue
    {
        readonly LogEventLevel _value;

        // Assumes reading from Dictionary<,> is thread-safe after construction
        static readonly Dictionary<LogEventLevel, string[]> _shortenedLevelMap = new Dictionary<LogEventLevel, string[]>
                                                                                  {
                                                                                      { LogEventLevel.Verbose, new []{ "V", "Vb", "Vrb", "Verb" } },
                                                                                      { LogEventLevel.Debug, new []{ "D", "De", "Dbg", "Dbug" } },
                                                                                      { LogEventLevel.Information, new []{ "I", "In", "Inf", "Info" } },
                                                                                      { LogEventLevel.Warning, new []{ "W", "Wn", "Wrn", "Warn" } },
                                                                                      { LogEventLevel.Error, new []{ "E", "Er", "Err", "Eror" } },
                                                                                      { LogEventLevel.Fatal, new []{ "F", "Fa", "Ftl", "Fatl" } },
                                                                                  };

        public LogEventLevelValue(
            LogEventLevel value)
        {
            _value = value;
        }

        /// <summary>
        /// This method will apply only upper or lower case formatting, not fixed width
        /// </summary>
        public override void Render(
            TextWriter output, 
            string format = null, 
            IFormatProvider formatProvider = null)
        {
            ApplyFormatting(output, _value.ToString(), null, format);
        }

        /// <summary>
        /// Will apply fixed width rules using the provided alignment
        /// </summary>
        public void Render(
            TextWriter output,
            Alignment? alignment,
            string format = null,
            IFormatProvider formatProvider = null)
        {
            ApplyFormatting(output, AlignedValue(alignment), alignment, format);
        }

        private string AlignedValue(Alignment? alignment)
        {
            var stringValue = _value.ToString();

            if (!alignment.HasValue || alignment.Value.Width <= 0)
            {
                return stringValue;
            }

            if (stringValue.Length == alignment.Value.Width)
            {
                return stringValue;
            }

            if (IsCustomWidthSupported(alignment.Value.Width))
            {
                return ShortLevelFor(_value, alignment.Value.Width);
            }

            if (IsOutputStringTooWide(alignment.Value, stringValue))
            {
                return stringValue.Substring(0, alignment.Value.Width);
            }

            return stringValue;
        }

        private void ApplyFormatting(TextWriter output, string value, Alignment? alignment, string format = null)
        {
            Padding.Apply(output, Casing.Format(value, format), alignment);
        }

        private static string ShortLevelFor(LogEventLevel value, int width)
        {
            return _shortenedLevelMap[value][width - 1];
        }

        private static bool IsOutputStringTooWide(Alignment alignmentValue, string formattedValue)
        {
            return alignmentValue.Width < formattedValue.Length;
        }

        private static bool IsCustomWidthSupported(int width)
        {
            return width > 0 && width < 5;
        }
    }
}