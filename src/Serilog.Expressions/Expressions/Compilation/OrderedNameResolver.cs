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

namespace Serilog.Expressions.Compilation;

class OrderedNameResolver : NameResolver
{
    readonly NameResolver[] _orderedResolvers;

    public OrderedNameResolver(IEnumerable<NameResolver> orderedResolvers)
    {
        _orderedResolvers = orderedResolvers.ToArray();
    }

    public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        foreach (var resolver in _orderedResolvers)
        {
            if (resolver.TryResolveFunctionName(name, out implementation))
                return true;
        }

        implementation = null;
        return false;
    }

    public override bool TryBindFunctionParameter(ParameterInfo parameter, [MaybeNullWhen(false)] out object boundValue)
    {
        foreach (var resolver in _orderedResolvers)
        {
            if (resolver.TryBindFunctionParameter(parameter, out boundValue))
                return true;
        }

        boundValue = null;
        return false;
    }

    public override bool TryResolveBuiltInPropertyName(string alias, [NotNullWhen(true)] out string? target)
    {
        foreach (var resolver in _orderedResolvers)
        {
            if (resolver.TryResolveBuiltInPropertyName(alias, out target))
                return true;
        }

        target = null;
        return false;
    }
}