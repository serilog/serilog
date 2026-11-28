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
using Serilog.Expressions.Runtime;
using Serilog.Parsing;
using Serilog.Templates.Ast;

namespace Serilog.Templates.Compilation.UnreferencedProperties;

/// <summary>
/// This little extension implements the <c>rest()</c> function in expression templates. It's based on
/// <c>Serilog.Sinks.SystemConsole.PropertiesTokenRenderer</c>, and is equivalent to how <c>Properties</c> is rendered by
/// the console sink. <c>rest()</c> will return a structure containing all of the user-defined properties from a
/// log event except those referenced in either the event's message template, or the expression template itself.
/// </summary>
/// <remarks>
/// The existing semantics of <c>Properties</c> in output templates isn't suitable for expression templates. The
/// <c>@p</c> object provides access to <em>all</em> event properties in an expression template, so it would make no
/// sense to render that object without all of its members.
/// </remarks>
class UnreferencedPropertiesFunction : NameResolver
{
    const string FunctionName = "rest";

    readonly HashSet<string> _referencedInTemplate;

    public UnreferencedPropertiesFunction(Template template)
    {
        var finder = new TemplateReferencedPropertiesFinder();
        _referencedInTemplate = [..finder.FindReferencedProperties(template)];
    }

    public override bool TryBindFunctionParameter(ParameterInfo parameter, [MaybeNullWhen(false)] out object boundValue)
    {
        if (parameter.ParameterType == typeof(UnreferencedPropertiesFunction))
        {
            boundValue = this;
            return true;
        }

        boundValue = null;
        return false;
    }

    public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        if (name.Equals(FunctionName, StringComparison.OrdinalIgnoreCase))
        {
            implementation = typeof(UnreferencedPropertiesFunction).GetMethod(nameof(Implementation),
                BindingFlags.Static | BindingFlags.Public)!;
            return true;
        }

        implementation = null;
        return false;
    }

    // By convention, built-in functions accept and return nullable values.
    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static LogEventPropertyValue? Implementation(UnreferencedPropertiesFunction self, LogEvent logEvent, LogEventPropertyValue? deep = null)
    {
        var checkMessageTemplate = Coerce.IsTrue(deep);
        return new StructureValue(logEvent.Properties
            .Where(kvp => !self._referencedInTemplate.Contains(kvp.Key) &&
                          (!checkMessageTemplate || !TemplateContainsPropertyName(logEvent.MessageTemplate, kvp.Key)))
            .Select(kvp => new LogEventProperty(kvp.Key, kvp.Value)));
    }

    static bool TemplateContainsPropertyName(MessageTemplate messageTemplate, string propertyName)
    {
        foreach (var token in messageTemplate.Tokens)
        {
            if (token is PropertyToken namedProperty &&
                namedProperty.PropertyName == propertyName)
            {
                return true;
            }
        }

        return false;
    }
}