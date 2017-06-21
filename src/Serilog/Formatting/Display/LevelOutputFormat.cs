// Copyright 2017 Serilog Contributors
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

using Serilog.Events;
using Serilog.Rendering;

namespace Serilog.Formatting.Display
{
    /// <summary>
    /// Implements the {Level} element.
    /// can now have a fixed width applied to it, as well as casing rules.
    /// Width is set through formats like "u3" (uppercase three chars),
    /// "w1" (one lowercase char), or "t4" (title case four chars).
    /// </summary>
    static class LevelOutputFormat
    {
        static readonly string[][] _titleCaseLevelMap = {
            new []{ "V", "Vb", "Vrb", "Verb" },
            new []{ "D", "De", "Dbg", "Dbug" },
            new []{ "I", "In", "Inf", "Info" },
            new []{ "W", "Wn", "Wrn", "Warn" },
            new []{ "E", "Er", "Err", "Eror" },
            new []{ "F", "Fa", "Ftl", "Fatl" }
        };

        static readonly string[][] _lowercaseLevelMap = {
            new []{ "v", "vb", "vrb", "verb" },
            new []{ "d", "de", "dbg", "dbug" },
            new []{ "i", "in", "inf", "info" },
            new []{ "w", "wn", "wrn", "warn" },
            new []{ "e", "er", "err", "eror" },
            new []{ "f", "fa", "ftl", "fatl" }
        };

        static readonly string[][] _uppercaseLevelMap = {
            new []{ "V", "VB", "VRB", "VERB" },
            new []{ "D", "DE", "DBG", "DBUG" },
            new []{ "I", "IN", "INF", "INFO" },
            new []{ "W", "WN", "WRN", "WARN" },
            new []{ "E", "ER", "ERR", "EROR" },
            new []{ "F", "FA", "FTL", "FATL" }
        };

        public static string GetLevelMoniker(LogEventLevel value, string format = null)
        {
            if (format == null || format.Length != 2 && format.Length != 3)
                return Casing.Format(value.ToString(), format);

            // Using int.Parse() here requires allocating a string to exclude the first character prefix.
            // Junk like "wxy" will be accepted but produce benign results.
            var width = format[1] - '0';
            if (format.Length == 3)
            {
                width *= 10;
                width += format[2] - '0';
            }

            if (width < 1)
                return string.Empty;

            if (width > 4)
            {
                var stringValue = value.ToString();
                if (stringValue.Length > width)
                    stringValue = stringValue.Substring(0, width);
                return Casing.Format(stringValue);
            }

            var index = (int)value;
            if (index >= 0 && index <= (int) LogEventLevel.Fatal)
            {
                switch (format[0])
                {
                    case 'w':
                        return _lowercaseLevelMap[index][width - 1];
                    case 'u':
                        return _uppercaseLevelMap[index][width - 1];
                    case 't':
                        return _titleCaseLevelMap[index][width - 1];
                }
            }

            return Casing.Format(value.ToString(), format);
        }
    }
}