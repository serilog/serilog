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
using Serilog.Events;

namespace Serilog.Expressions.Runtime.Support;

/// <summary>
/// Nest (un-flatten) properties with dotted names. A property with name <c>"a.b"</c> will be transmitted to Seq as
/// a structure with name <c>"a"</c>, and one member <c>"b"</c>.
/// </summary>
/// <remarks>Forked from <a href="https://github.com/datalust/serilog-sinks-seq/blob/6815d7307b477747e05e782f7dfbcff8c8dd20a2/src/Serilog.Sinks.Seq/Sinks/Seq/Conventions/UnflattenDottedPropertyNames.cs"/>.</remarks>
static class UnflattenDottedPropertyNames
{
    const int MaxDepth = 10;

    public static IReadOnlyDictionary<string, LogEventPropertyValue> ProcessDottedPropertyNames(IReadOnlyDictionary<string, LogEventPropertyValue> maybeDotted)
    {
        return DottedToNestedRecursive(maybeDotted, 0);
    }

    static IReadOnlyDictionary<string, LogEventPropertyValue> DottedToNestedRecursive(IReadOnlyDictionary<string, LogEventPropertyValue> maybeDotted, int depth)
    {
        if (depth == MaxDepth)
            return maybeDotted;

        // Assume that the majority of entries will be bare or have unique prefixes.
        var result = new Dictionary<string, LogEventPropertyValue>(maybeDotted.Count);

        // Sorted for determinism.
        var dotted = new SortedDictionary<string, LogEventPropertyValue>(StringComparer.Ordinal);

        // First - give priority to bare names, since these would otherwise be claimed by the parents of further nested
        // layers and we'd have nowhere to put them when resolving conflicts. (Dotted entries that conflict can keep their dotted keys).

        foreach (var kv in maybeDotted)
        {
            if (IsDottedIdentifier(kv.Key))
            {
                // Stash for processing in the next stage.
                dotted.Add(kv.Key, kv.Value);
            }
            else
            {
                result.Add(kv.Key, kv.Value);
            }
        }

        // Then - for dotted keys with a prefix not already present in the result, convert to structured data and add to
        // the result. Any set of dotted names that collide with a preexisting key will be left as-is.

        string? prefix = null;
        Dictionary<string, LogEventPropertyValue>? nested = null;
        foreach (var kv in dotted)
        {
            var (newPrefix, rem) = TakeFirstIdentifier(kv.Key);

            if (prefix != null && prefix != newPrefix)
            {
                result.Add(prefix, MakeStructureValue(DottedToNestedRecursive(nested!, depth + 1)));
                prefix = null;
                nested = null;
            }

            if (nested != null && !nested.ContainsKey(rem))
            {
                prefix = newPrefix;
                nested.Add(rem, kv.Value);
            }
            else if (nested == null && !result.ContainsKey(newPrefix))
            {
                prefix = newPrefix;
                nested = new () { { rem, kv.Value } };
            }
            else
            {
                result.Add(kv.Key, kv.Value);
            }
        }

        if (prefix != null)
        {
            result[prefix] = MakeStructureValue(DottedToNestedRecursive(nested!, depth + 1));
        }

        return result;
    }

    static StructureValue MakeStructureValue(IReadOnlyDictionary<string,LogEventPropertyValue> properties)
    {
        return new StructureValue(properties.Select(kv => new LogEventProperty(kv.Key, kv.Value)), typeTag: null);
    }

    static bool IsDottedIdentifier(string key) =>
        key.Contains('.') &&
        !key.StartsWith(".", StringComparison.Ordinal) &&
        !key.EndsWith(".", StringComparison.Ordinal) &&
        key.Split('.').All(IsIdentifier);

    static bool IsIdentifier(string s) => s.Length != 0 &&
                                          !char.IsDigit(s[0]) &&
                                          s.All(ch => char.IsLetter(ch) || char.IsDigit(ch) || ch == '_');

    static (string, string) TakeFirstIdentifier(string dottedIdentifier)
    {
        // We can do this simplistically because keys in `dotted` conform to `IsDottedName`.
        Debug.Assert(IsDottedIdentifier(dottedIdentifier));

        var firstDot = dottedIdentifier.IndexOf('.');
        var prefix = dottedIdentifier.Substring(0, firstDot);
        var rem = dottedIdentifier.Substring(firstDot + 1);
        return (prefix, rem);
    }
}
