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
using System.Reflection;
using Serilog.Events;
using Serilog.Expressions;
using Serilog.Templates.Encoding;

namespace Serilog.Templates.Compilation.Unsafe;

/// <summary>
/// Marks an expression in a template as bypassing the output encoding mechanism.
/// </summary>
class UnsafeOutputFunction : NameResolver
{
    const string FunctionName = "unsafe";

    public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        if (name.Equals(FunctionName, StringComparison.OrdinalIgnoreCase))
        {
            implementation = typeof(UnsafeOutputFunction).GetMethod(nameof(Implementation),
                BindingFlags.Static | BindingFlags.Public)!;
            return true;
        }

        implementation = null;
        return false;
    }

    // By convention, built-in functions accept and return nullable values.
    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? Implementation(LogEventPropertyValue? inner)
    {
        return new ScalarValue(new PreEncodedValue(inner));
    }
}
