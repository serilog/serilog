// Copyright 2016 Datalust, Superpower Contributors, Sprache Contributors
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

using System.Reflection;
using Serilog.ParserConstruction.Util;

namespace Serilog.ParserConstruction.Display;

static class Presentation
{
    static string FormatKind(object kind)
    {
        return kind.ToString()!.ToLower();
    }

    static TokenAttribute? TryGetTokenAttribute(Type type)
    {
        return type.GetTypeInfo().GetCustomAttribute<TokenAttribute>();
    }

    static TokenAttribute? TryGetTokenAttribute<TKind>(TKind kind)
    {
        var kindTypeInfo = typeof(TKind).GetTypeInfo();
        if (kindTypeInfo.IsEnum)
        {
            var field = kindTypeInfo.GetDeclaredField(kind!.ToString()!);
            if (field != null)
            {
                return field.GetCustomAttribute<TokenAttribute>() ?? TryGetTokenAttribute(typeof(TKind));
            }
        }

        return TryGetTokenAttribute(typeof(TKind));
    }

    public static string FormatExpectation<TKind>(TKind kind)
    {
        var description = TryGetTokenAttribute(kind);
        if (description != null)
        {
            if (description.Description != null)
                return description.Description;
            if (description.Example != null)
                return FormatLiteral(description.Example);
        }

        return FormatKind(kind!);
    }

    public static string FormatAppearance<TKind>(TKind kind, string value)
    {
        var clipped = FormatLiteral(Friendly.Clip(value, 12));

        var description = TryGetTokenAttribute(kind);
        if (description != null)
        {
            if (description.Category != null)
                return $"{description.Category} {clipped}";

            if (description.Example != null)
                return clipped;
        }

        return $"{FormatKind(kind!)} {clipped}";
    }
    public static string FormatLiteral(char literal)
    {
        switch (literal)
        {
            //Unicode Category: Space Separators
            case '\x00A0': return "U+00A0 no-break space";
            case '\x1680': return "U+1680 ogham space mark";
            case '\x2000': return "U+2000 en quad";
            case '\x2001': return "U+2001 em quad";
            case '\x2002': return "U+2002 en space";
            case '\x2003': return "U+2003 em space";
            case '\x2004': return "U+2004 three-per-em space";
            case '\x2005': return "U+2005 four-per-em space";
            case '\x2006': return "U+2006 six-per-em space";
            case '\x2007': return "U+2007 figure space";
            case '\x2008': return "U+2008 punctuation space";
            case '\x2009': return "U+2009 thin space";
            case '\x200A': return "U+200A hair space";
            case '\x202F': return "U+202F narrow no-break space";
            case '\x205F': return "U+205F medium mathematical space";
            case '\x3000': return "U+3000 ideographic space";

            //Line Separator
            case '\x2028': return "U+2028 line separator";

            //Paragraph Separator
            case '\x2029': return "U+2029 paragraph separator";

            //Unicode C0 Control Codes (ASCII equivalent)
            case '\x0000': return "NUL"; //\0
            case '\x0001': return "U+0001 start of heading";
            case '\x0002': return "U+0002 start of text";
            case '\x0003': return "U+0003 end of text";
            case '\x0004': return "U+0004 end of transmission";
            case '\x0005': return "U+0005 enquiry";
            case '\x0006': return "U+0006 acknowledge";
            case '\x0007': return "U+0007 bell";
            case '\x0008': return "U+0008 backspace";
            case '\x0009': return "tab"; //\t
            case '\x000A': return "line feed"; //\n
            case '\x000B': return "U+000B vertical tab";
            case '\x000C': return "U+000C form feed";
            case '\x000D': return "carriage return"; //\r
            case '\x000E': return "U+000E shift in";
            case '\x000F': return "U+000F shift out";
            case '\x0010': return "U+0010 data link escape";
            case '\x0011': return "U+0011 device ctrl 1";
            case '\x0012': return "U+0012 device ctrl 2";
            case '\x0013': return "U+0013 device ctrl 3";
            case '\x0014': return "U+0014 device ctrl 4";
            case '\x0015': return "U+0015 not acknowledge";
            case '\x0016': return "U+0016 synchronous idle";
            case '\x0017': return "U+0017 end transmission block";
            case '\x0018': return "U+0018 cancel";
            case '\x0019': return "U+0019 end of medium";
            case '\x0020': return "space";
            case '\x001A': return "U+001A substitute";
            case '\x001B': return "U+001B escape";
            case '\x001C': return "U+001C file separator";
            case '\x001D': return "U+001D group separator";
            case '\x001E': return "U+001E record separator";
            case '\x001F': return "U+001F unit separator";
            case '\x007F': return "U+007F delete";

            default: return "`" + literal + "`";
        }
    }

    public static string FormatLiteral(string literal)
    {
        return "`" + literal + "`";
    }
}