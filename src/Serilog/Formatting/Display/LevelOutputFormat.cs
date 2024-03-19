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

namespace Serilog.Formatting.Display;

/// <summary>
/// Implements the {Level} element.
/// can now have a fixed width applied to it, as well as casing rules.
/// Width is set through formats like "u3" (uppercase three chars),
/// "w1" (one lowercase char), or "t4" (title case four chars).
/// </summary>
static class LevelOutputFormat
{
    static readonly string[][] _titleCaseLevelMap = [
        ["V", "Vb", "Vrb", "Verb", "Verbo", "Verbos", "Verbose"],
        ["D", "De", "Dbg", "Dbug", "Debug"],
        ["I", "In", "Inf", "Info", "Infor", "Inform", "Informa", "Informat", "Informati", "Informatio", "Information"],
        ["W", "Wn", "Wrn", "Warn", "Warni", "Warnin", "Warning"],
        ["E", "Er", "Err", "Eror", "Error"],
        ["F", "Fa", "Ftl", "Fatl", "Fatal"]
    ];

    static readonly string[][] _lowerCaseLevelMap = [
        ["v", "vb", "vrb", "verb", "verbo", "verbos", "verbose"],
        ["d", "de", "dbg", "dbug", "debug"],
        ["i", "in", "inf", "info", "infor", "inform", "informa", "informat", "informati", "informatio", "information"],
        ["w", "wn", "wrn", "warn", "warni", "warnin", "warning"],
        ["e", "er", "err", "eror", "error"],
        ["f", "fa", "ftl", "fatl", "fatal"]
    ];

    static readonly string[][] _upperCaseLevelMap = [
        ["V", "VB", "VRB", "VERB", "VERBO", "VERBOS", "VERBOSE"],
        ["D", "DE", "DBG", "DBUG", "DEBUG"],
        ["I", "IN", "INF", "INFO", "INFOR", "INFORM", "INFORMA", "INFORMAT", "INFORMATI", "INFORMATIO", "INFORMATION"],
        ["W", "WN", "WRN", "WARN", "WARNI", "WARNIN", "WARNING"],
        ["E", "ER", "ERR", "EROR", "ERROR"],
        ["F", "FA", "FTL", "FATL", "FATAL"]
    ];

    public static string GetLevelMoniker(LogEventLevel value, string? format = null)
    {
        // handle unknown LogEventLevel
        if (value is < 0 or > LogEventLevel.Fatal)
            return Casing.Format(value.ToString(), format);

        if (format == null || format.Length != 2 && format.Length != 3)
            return Casing.Format(GetLevelMoniker(_titleCaseLevelMap, value), format);

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

        return format[0] switch
        {
            'w' => GetLevelMoniker(_lowerCaseLevelMap, value, width),
            'u' => GetLevelMoniker(_upperCaseLevelMap, value, width),
            't' => GetLevelMoniker(_titleCaseLevelMap, value, width),
            _ => Casing.Format(GetLevelMoniker(_titleCaseLevelMap, value), format)
        };
    }

    static string GetLevelMoniker(string[][] caseLevelMap, LogEventLevel level, int width)
    {
        var caseLevel = caseLevelMap[(int)level];
        return caseLevel[Math.Min(width, caseLevel.Length) - 1];
    }

    static string GetLevelMoniker(string[][] caseLevelMap, LogEventLevel level)
    {
        var caseLevel = caseLevelMap[(int)level];
        return caseLevel[caseLevel.Length - 1];
    }
}
