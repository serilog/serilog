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

using System.Globalization;
using Serilog.Events;

namespace Serilog.Expressions.Ast;

/// <summary>
/// A constant such as <code>'hello'</code>, <code>true</code>, <code>null</code>, or <code>123.45</code>.
/// </summary>
class ConstantExpression : Expression
{
    public ConstantExpression(LogEventPropertyValue constant)
    {
        Constant = constant ?? throw new ArgumentNullException(nameof(constant));
    }

    public LogEventPropertyValue Constant { get; }

    public override string ToString()
    {
        if (Constant is ScalarValue sv)
        {
            return sv.Value switch
            {
                string s => "'" + s.Replace("'", "''") + "'",
                true => "true",
                false => "false",
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
                _ => (sv.Value ?? "null").ToString() ?? "<ToString() returned null>"
            };
        }

        return Constant.ToString();
    }
}