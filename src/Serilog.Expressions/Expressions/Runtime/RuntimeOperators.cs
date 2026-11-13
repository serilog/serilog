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

using System.Reflection;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Expressions.Compilation.Linq;
using Serilog.Expressions.Runtime.Support;
using Serilog.Templates.Rendering;

// ReSharper disable ForCanBeConvertedToForeach, InvertIf, MemberCanBePrivate.Global, UnusedMember.Global, InconsistentNaming, ReturnTypeCanBeNotNullable

namespace Serilog.Expressions.Runtime;

static class RuntimeOperators
{
    static readonly LogEventPropertyValue ConstantTrue = new ScalarValue(true),
        ConstantFalse = new ScalarValue(false);

    internal static LogEventPropertyValue ScalarBoolean(bool value)
    {
        return value ? ConstantTrue : ConstantFalse;
    }

    public static LogEventPropertyValue? Undefined()
    {
        return null;
    }

    public static LogEventPropertyValue? _Internal_Add(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return new ScalarValue(l + r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Subtract(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return new ScalarValue(l - r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Multiply(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return new ScalarValue(l * r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Divide(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r) &&
            r != 0)
            return new ScalarValue(l / r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Modulo(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r) &&
            r != 0)
            return new ScalarValue(l % r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Power(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return new ScalarValue(Math.Pow((double)l, (double)r));

        return default;
    }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? _Internal_And(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        throw new InvalidOperationException("Logical operators should be evaluated intrinsically.");
    }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? _Internal_Or(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        throw new InvalidOperationException("Logical operators should be evaluated intrinsically.");
    }

    public static LogEventPropertyValue? _Internal_LessThanOrEqual(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return ScalarBoolean(l <= r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_LessThan(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return ScalarBoolean(l < r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_GreaterThan(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return ScalarBoolean(l > r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_GreaterThanOrEqual(LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return ScalarBoolean(l >= r);

        return default;
    }

    public static LogEventPropertyValue? _Internal_Equal(StringComparison sc, LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        // Undefined values propagate through comparisons
        if (left == null || right == null)
            return null;

        return ScalarBoolean(UnboxedEqualHelper(sc, left, right));
    }

    // Return value is a regular `bool` and not a scalar value as you'd get from `Equal`
    static bool UnboxedEqualHelper(StringComparison sc, LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (left == null || right == null)
            throw new ArgumentException("Undefined values should short-circuit.");

        if (Coerce.Numeric(left, out var l) &&
            Coerce.Numeric(right, out var r))
            return l == r;

        if (Coerce.String(left, out var ls) &&
            Coerce.String(right, out var rs))
            return ls.Equals(rs, sc);

        if (left is ScalarValue sl &&
            right is ScalarValue sr)
            return sl.Value?.Equals(sr.Value) ?? sr.Value == null;

        if (left is SequenceValue ql &&
            right is SequenceValue qr)
        {
            // Not in any way optimized :-)
            return ql.Elements.Count == qr.Elements.Count &&
                   ql.Elements.Zip(qr.Elements, (ll, rr) => UnboxedEqualHelper(sc, ll, rr)).All(eq => eq);
        }

        if (left is StructureValue tl &&
            right is StructureValue tr)
        {
            // .... even less optimized; lots of work to de-dup keys with last-in-wins precedence.
            var lhs = new Dictionary<string, LogEventPropertyValue?>();
            foreach (var property in tl.Properties)
                lhs[property.Name] = property.Value;

            var rhs = new Dictionary<string, LogEventPropertyValue?>();
            foreach (var property in tr.Properties)
                rhs[property.Name] = property.Value;

            return lhs.Keys.Count == rhs.Keys.Count &&
                   lhs.All(kv => rhs.TryGetValue(kv.Key, out var value) &&
                                 UnboxedEqualHelper(sc, kv.Value, value));
        }

        return false;
    }

    public static LogEventPropertyValue? _Internal_In(StringComparison sc, LogEventPropertyValue? item, LogEventPropertyValue? collection)
    {
        if (item == null)
            return null;

        if (collection is SequenceValue arr)
        {
            for (var i = 0; i < arr.Elements.Count; ++i)
            {
                var element = arr.Elements[i];
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (element != null && UnboxedEqualHelper(sc, element, item))
                    return ConstantTrue;
            }

            return ConstantFalse;
        }

        return null;
    }

    public static LogEventPropertyValue? _Internal_NotIn(StringComparison sc, LogEventPropertyValue? item, LogEventPropertyValue? collection)
    {
        return _Internal_StrictNot(_Internal_In(sc, item, collection));
    }

    public static LogEventPropertyValue? _Internal_NotEqual(StringComparison sc, LogEventPropertyValue? left, LogEventPropertyValue? right)
    {
        if (left == null || right == null)
            return null;

        return ScalarBoolean(!UnboxedEqualHelper(sc, left, right));
    }

    public static LogEventPropertyValue? _Internal_Negate(LogEventPropertyValue? value)
    {
        if (Coerce.Numeric(value, out var numeric))
            return new ScalarValue(-numeric);
        return null;
    }

    public static LogEventPropertyValue? Round(LogEventPropertyValue? value, LogEventPropertyValue? places)
    {
        if (!Coerce.Numeric(value, out var v) ||
            !Coerce.Numeric(places, out var p) ||
            p is < 0 or > 32) // Check my memory, here :D
        {
            return null;
        }

        return new ScalarValue(Math.Round(v, (int)p));
    }

    public static LogEventPropertyValue? _Internal_Not(LogEventPropertyValue? value)
    {
        if (value is null)
            return ConstantTrue;

        return Coerce.Boolean(value, out var b) ?
            ScalarBoolean(!b) :
            null;
    }

    public static LogEventPropertyValue? _Internal_StrictNot(LogEventPropertyValue? value)
    {
        return Coerce.Boolean(value, out var b) ?
            ScalarBoolean(!b) :
            null;
    }

    public static LogEventPropertyValue? Contains(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        if (!Coerce.String(haystack, out var ctx) ||
            !Coerce.String(needle, out var ptx))
            return null;

        return ScalarBoolean(ctx.Contains(ptx, sc));
    }

    public static LogEventPropertyValue? IndexOf(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        if (!Coerce.String(haystack, out var ctx) ||
            !Coerce.String(needle, out var ptx))
            return null;

        return new ScalarValue(ctx.IndexOf(ptx, sc));
    }

    public static LogEventPropertyValue? LastIndexOf(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        if (!Coerce.String(haystack, out var ctx) ||
            !Coerce.String(needle, out var ptx))
            return null;

        return new ScalarValue(ctx.LastIndexOf(ptx, sc));
    }

    public static LogEventPropertyValue? Length(LogEventPropertyValue? value)
    {
        if (Coerce.String(value, out var s))
            return new ScalarValue(s.Length);

        if (value is SequenceValue seq)
            return new ScalarValue(seq.Elements.Count);

        return null;
    }

    public static LogEventPropertyValue? StartsWith(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        if (!Coerce.String(haystack, out var ctx) ||
            !Coerce.String(needle, out var ptx))
            return null;

        return ScalarBoolean(ctx.StartsWith(ptx, sc));
    }

    public static LogEventPropertyValue? EndsWith(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        if (!Coerce.String(haystack, out var ctx) ||
            !Coerce.String(needle, out var ptx))
            return null;

        return ScalarBoolean(ctx.EndsWith(ptx, sc));
    }

    public static LogEventPropertyValue IsDefined(LogEventPropertyValue? value)
    {
        return ScalarBoolean(value != null);
    }

    public static LogEventPropertyValue? ElementAt(StringComparison sc, LogEventPropertyValue? collection, LogEventPropertyValue? index)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (collection is SequenceValue arr && Coerce.Numeric(index, out var ix))
        {
            if (ix != Math.Floor(ix))
                return null;

            var idx = (int)ix;
            if (idx >= arr.Elements.Count)
                return null;

            return arr.Elements.ElementAt(idx);
        }

        if (collection is StructureValue st && Coerce.String(index, out var s))
        {
            return Intrinsics.TryGetStructurePropertyValue(sc, st, s);
        }

        if (collection is DictionaryValue dict && index is ScalarValue sv)
        {
            // The lack of eager numeric type coercion means that here, `sv` may logically equal one
            // of the keys, but not be equal according to the dictionary's `IEqualityComparer`.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            var entry = dict.Elements.FirstOrDefault(kv => kv.Key != null && UnboxedEqualHelper(sc, kv.Key, sv));
            return entry.Value; // KVP is a struct; default is a pair of nulls.
        }

        return null;
    }

    public static LogEventPropertyValue? _Internal_Any(LogEventPropertyValue? collection, LogEventPropertyValue? predicate)
    {
        if (!Coerce.Predicate(predicate, out var pred))
            return null;

        if (collection is SequenceValue arr)
        {
            return ScalarBoolean(arr.Elements.Any(e => Coerce.IsTrue(pred(e))));
        }

        if (collection is StructureValue structure)
        {
            return ScalarBoolean(structure.Properties.Any(e => Coerce.IsTrue(pred(e.Value))));
        }

        if (collection is DictionaryValue dictionary)
        {
            return ScalarBoolean(dictionary.Elements.Any(e => Coerce.IsTrue(pred(e.Value))));
        }

        return null;
    }

    public static LogEventPropertyValue? _Internal_All(LogEventPropertyValue? collection, LogEventPropertyValue? predicate)
    {
        if (!Coerce.Predicate(predicate, out var pred))
            return null;

        if (collection is SequenceValue arr)
        {
            return ScalarBoolean(arr.Elements.All(e => Coerce.IsTrue(pred(e))));
        }

        if (collection is StructureValue structure)
        {
            return ScalarBoolean(structure.Properties.All(e => Coerce.IsTrue(pred(e.Value))));
        }

        if (collection is DictionaryValue dictionary)
        {
            return ScalarBoolean(dictionary.Elements.All(e => Coerce.IsTrue(pred(e.Value))));
        }

        return null;
    }

    public static LogEventPropertyValue? TagOf(LogEventPropertyValue? value)
    {
        if (value is StructureValue structure)
            return new ScalarValue(structure.TypeTag); // I.e. may be null

        return null;
    }

    public static LogEventPropertyValue TypeOf(LogEventPropertyValue? value)
    {
        if (value is DictionaryValue)
            return new ScalarValue("dictionary");

        if (value is StructureValue)
            return new ScalarValue("object");

        if (value is SequenceValue)
            return new ScalarValue("array");

        if (value is ScalarValue scalar)
        {
            return new ScalarValue(scalar.Value?.GetType().ToString() ?? "null");
        }

        return new ScalarValue("undefined");
    }

    public static LogEventPropertyValue _Internal_IsNull(LogEventPropertyValue? value)
    {
        return ScalarBoolean(value is null or ScalarValue {Value: null});
    }

    public static LogEventPropertyValue _Internal_IsNotNull(LogEventPropertyValue? value)
    {
        return ScalarBoolean(value is not (null or ScalarValue {Value: null}));
    }

    // Ideally this will be compiled as a short-circuiting intrinsic
    public static LogEventPropertyValue? Coalesce(LogEventPropertyValue? value0, LogEventPropertyValue? value1)
    {
        if (value0 is null or ScalarValue {Value: null})
            return value1;

        return value0;
    }

    public static LogEventPropertyValue? Substring(LogEventPropertyValue? value, LogEventPropertyValue? startIndex, LogEventPropertyValue? length = null)
    {
        if (!Coerce.String(value, out var str) ||
            !Coerce.Numeric(startIndex, out var si))
            return null;

        if (si < 0 || si >= str.Length || (int)si != si)
            return null;

        if (length == null)
            return new ScalarValue(str.Substring((int)si));

        if (!Coerce.Numeric(length, out var len) || (int)len != len)
            return null;

        if (len + si > str.Length)
            return new ScalarValue(str.Substring((int)si));

        return new ScalarValue(str.Substring((int)si, (int)len));
    }

    public static LogEventPropertyValue? Concat(LogEventPropertyValue? string0, LogEventPropertyValue? string1)
    {
        if (Coerce.String(string0, out var f) && Coerce.String(string1, out var s))
        {
            return new ScalarValue(f + s);
        }

        return null;
    }

    public static LogEventPropertyValue? Replace(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle, LogEventPropertyValue? replacement)
    {
        if (Coerce.String(haystack, out var h) && Coerce.String(needle, out var n) && Coerce.String(replacement, out var r))
        {
            return new ScalarValue(h.Replace(n, r, sc));
        }

        return null;
    }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? IndexOfMatch(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        throw new InvalidOperationException("Regular expression evaluation is intrinsic.");
    }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? IsMatch(StringComparison sc, LogEventPropertyValue? haystack, LogEventPropertyValue? needle)
    {
        throw new InvalidOperationException("Regular expression evaluation is intrinsic.");
    }

    // Ideally this will be compiled as a short-circuiting intrinsic
    public static LogEventPropertyValue? _Internal_IfThenElse(
        LogEventPropertyValue? condition,
        LogEventPropertyValue? consequent,
        LogEventPropertyValue? alternative)
    {
        return Coerce.IsTrue(condition) ? consequent : alternative;
    }

    public static LogEventPropertyValue? ToString(IFormatProvider? formatProvider, LogEventPropertyValue? value, LogEventPropertyValue? format = null)
    {
        if (value is not ScalarValue sv ||
            sv.Value == null ||
            !(Coerce.String(format, out var fmt) || format is null or ScalarValue { Value: null }))
        {
            return null;
        }

        var toString = sv.Value switch
        {
            LogEventLevel level => LevelRenderer.GetLevelMoniker(level, fmt),
            IFormattable formattable => formattable.ToString(fmt, formatProvider),
            _ => sv.Value.ToString()
        };

        return new ScalarValue(toString);
    }

    public static LogEventPropertyValue? UtcDateTime(LogEventPropertyValue? dateTime)
    {
        if (dateTime is ScalarValue sv)
        {
            if (sv.Value is DateTimeOffset dto)
                return new ScalarValue(dto.UtcDateTime);

            if (sv.Value is DateTime dt)
                return new ScalarValue(dt.ToUniversalTime());
        }

        return null;
    }

    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? Now()
    {
        // DateTimeOffset.Now is the generator for LogEvent.Timestamp.
        return new ScalarValue(DateTimeOffset.Now);
    }

    public static LogEventPropertyValue? Inspect(LogEventPropertyValue? value, LogEventPropertyValue? deep = null)
    {
        if (value is not ScalarValue { Value: {} toCapture })
            return value;

        var result = new List<LogEventProperty>();
        var logger = new LoggerConfiguration().CreateLogger();
        var properties = toCapture.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

        foreach (var property in properties)
        {
            object? p;
            try
            {
                p = property.GetValue(toCapture);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Serilog.Expressions Inspect() target property threw exception: {0}", ex);
                continue;
            }
            
            if (deep is ScalarValue { Value: true })
            {
                if (logger.BindProperty(property.Name, p, destructureObjects: true, out var bound))
                    result.Add(bound);
            }
            else
            {
                result.Add(new LogEventProperty(property.Name, new ScalarValue(p)));
            }
        }

        return new StructureValue(result);
    }

    public static LogEventPropertyValue? Nest(LogEventPropertyValue? maybeStructure)
    {
        if (maybeStructure is not StructureValue { Properties: { } flat })
            return null;

        var byName = new Dictionary<string, LogEventPropertyValue>(flat.Count);
        foreach (var property in flat)
        {
            // Supports duplicate property names, despite these being hard to generate.
            byName[property.Name] = property.Value;
        }
        
        var props = UnflattenDottedPropertyNames.ProcessDottedPropertyNames(byName);
        return new StructureValue(props.Select(p => new LogEventProperty(p.Key, p.Value)).ToList());
    }
}
