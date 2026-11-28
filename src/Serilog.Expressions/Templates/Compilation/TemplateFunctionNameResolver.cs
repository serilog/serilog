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

using Serilog.Expressions;
using Serilog.Expressions.Compilation;
using Serilog.Expressions.Runtime;
using Serilog.Templates.Ast;
using Serilog.Templates.Compilation.UnreferencedProperties;
using Serilog.Templates.Compilation.Unsafe;

namespace Serilog.Templates.Compilation;

static class TemplateFunctionNameResolver
{
    public static NameResolver Build(NameResolver? additionalNameResolver, Template template)
    {
        var resolvers = new List<NameResolver>
        {
            new StaticMemberNameResolver(typeof(RuntimeOperators)),
            new UnreferencedPropertiesFunction(template),
            new UnsafeOutputFunction()
        };

        if (additionalNameResolver != null)
            resolvers.Add(additionalNameResolver);

        return new OrderedNameResolver(resolvers);
    }
}