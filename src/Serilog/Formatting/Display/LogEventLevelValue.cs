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
        private readonly LogEventLevel _value;
        private readonly Alignment? _alignment;

        private Dictionary<LogEventLevel, string[]> _shortenedLevelMap
            = new Dictionary<LogEventLevel, string[]>
                  {
                      { LogEventLevel.Verbose, new []{ "V", "Vb", "Vrb", "Verb" } },
                      { LogEventLevel.Debug, new []{ "D", "De", "Dbg", "Dbug" } },
                      { LogEventLevel.Information, new []{ "I", "In", "Inf", "Info" } },
                      { LogEventLevel.Warning, new []{ "W", "Wn", "Wrn", "Warn" } },
                      { LogEventLevel.Error, new []{ "E", "Er", "Err", "Eror" } },
                      { LogEventLevel.Fatal, new []{ "F", "Fa", "Ftl", "Fatl" } },
                  };

        public LogEventLevelValue(
            LogEventLevel value,
            Alignment? alignment)
        {
            _value = value;
            _alignment = alignment;
        }

        public string FormattedValue()
        {
            var formattedValue = _value.ToString();

            if (!_alignment.HasValue || _alignment.Value.Width <= 0)
            {
                return formattedValue;
            }

            var alignmentValue = _alignment.Value;

            if (formattedValue.Length == alignmentValue.Width)
            {
                return formattedValue;
            }

            if (IsCustomWidthSupported(alignmentValue.Width))
            {
                return ShortLevelFor(alignmentValue);
            }

            if (IsOutputStringTooWide(alignmentValue, formattedValue))
            {
                return formattedValue.Substring(0, alignmentValue.Width);
            }

            return formattedValue;
        }

        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            new LiteralStringValue(FormattedValue()).Render(output, format, formatProvider);
        }

        private string ShortLevelFor(Alignment alignmentValue)
        {
            return _shortenedLevelMap[_value][alignmentValue.Width - 1];
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