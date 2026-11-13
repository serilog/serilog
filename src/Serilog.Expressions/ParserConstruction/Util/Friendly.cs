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

namespace Serilog.ParserConstruction.Util;

static class Friendly
{
    public static string List(IEnumerable<string> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        // Keep the order stable
        var seen = new HashSet<string>();
        var unique = new List<string>();
        foreach (var item in items)
        {
            if (seen.Contains(item)) continue;
            seen.Add(item);
            unique.Add(item);
        }

        if (unique.Count == 0)
            throw new ArgumentException("Friendly list formatting requires at least one element.", nameof(items));

        if (unique.Count == 1)
            return unique.Single();

        return $"{string.Join(", ", unique.Take(unique.Count - 1))} or {unique.Last()}";
    }

    public static string Clip(string value, int maxLength)
    {
        if (value.Length > maxLength)
            return value.Substring(0, maxLength - 3) + "...";
        return value;
    }
}