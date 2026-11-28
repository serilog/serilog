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

using System.Diagnostics.CodeAnalysis;
using Serilog.Events;

namespace Serilog.Expressions.Runtime;

/// <summary>
/// Named local variables. We just look them up by name. The structure is a
/// linked list with a null terminator: most of the time expressions won't have any
/// locals, and when they do, they'll only have one or two at a given point.
/// </summary>
class Locals
{
    readonly Locals? _others;
    readonly string _name;
    readonly LogEventPropertyValue _value;

    Locals(Locals? others, string name, LogEventPropertyValue value)
    {
        _others = others;
        _name = name;
        _value = value;
    }

    public static Locals Set(Locals? others, string name, LogEventPropertyValue value)
    {
        return new(others, name, value);
    }

    public static bool TryGetValue(Locals? locals, string name, [MaybeNullWhen(false)] out LogEventPropertyValue value)
    {
        while (locals != null)
        {
            if (name == locals._name)
            {
                value = locals._value;
                return true;
            }

            locals = locals._others;
        }

        value = null;
        return false;
    }
}