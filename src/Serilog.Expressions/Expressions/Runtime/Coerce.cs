// Copyright © Serilog Contributors
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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Serilog.Events;

namespace Serilog.Expressions.Runtime;

static class Coerce
{
    static readonly Type[] NumericTypes =
    [
        typeof(decimal),
        typeof(int), typeof(long), typeof(double),
        typeof(float), typeof(uint), typeof(sbyte),
        typeof(byte), typeof(short), typeof(ushort), typeof(ulong)
    ];

    public static bool Numeric(LogEventPropertyValue? value, out decimal numeric)
    {
        if (value is ScalarValue sv &&
            sv.Value != null &&
            NumericTypes.Contains(sv.Value.GetType()))
        {
            numeric = (decimal)Convert.ChangeType(sv.Value, typeof(decimal));
            return true;
        }

        numeric = default;
        return false;
    }

    public static bool Boolean(LogEventPropertyValue? value, out bool boolean)
    {
        if (value is ScalarValue sv &&
            sv.Value is bool b)
        {
            boolean = b;
            return true;
        }

        boolean = default;
        return false;
    }

    public static bool IsTrue(LogEventPropertyValue? value)
    {
        return Boolean(value, out var b) && b;
    }

    public static bool String(LogEventPropertyValue? value, [NotNullWhen(true)] out string? str)
    {
        if (value is ScalarValue sv)
        {
            if (sv.Value is string s)
            {
                str = s;
                return true;
            }

            if (sv.Value is Exception ex)
            {
                str = ex.ToString();
                return true;
            }

            if (sv.Value?.GetType().IsEnum ?? false)
            {
                str = sv.Value.ToString()!;
                return true;
            }

            if (sv.Value is ActivityTraceId traceId)
            {
                str = traceId.ToHexString();
                return true;
            }

            if (sv.Value is ActivitySpanId spanId)
            {
                str = spanId.ToHexString();
                return true;
            }
        }

        str = default;
        return false;
    }

    public static bool Predicate(LogEventPropertyValue? value,
        [MaybeNullWhen(false)] out Func<LogEventPropertyValue, LogEventPropertyValue> predicate)
    {
        if (value is ScalarValue sv &&
            sv.Value is Func<LogEventPropertyValue, LogEventPropertyValue> pred)
        {
            predicate = pred;
            return true;
        }

        predicate = default;
        return false;
    }
}