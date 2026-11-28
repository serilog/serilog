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

using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Transformations;

namespace Serilog.Expressions.Compilation.Properties;

class PropertiesObjectAccessorTransformer : IdentityTransformer
{
    public static Expression Rewrite(Expression actual)
    {
        return new PropertiesObjectAccessorTransformer().Transform(actual);
    }

    protected override Expression Transform(AccessorExpression ax)
    {
        if (!Pattern.IsAmbientProperty(ax.Receiver, BuiltInProperty.Properties, true))
            return base.Transform(ax);

        return new AmbientNameExpression(ax.MemberName, false);
    }

    protected override Expression Transform(IndexerExpression ix)
    {
        if (!Pattern.IsAmbientProperty(ix.Receiver, BuiltInProperty.Properties, true) ||
            !Pattern.IsStringConstant(ix.Index, out var name))
            return base.Transform(ix);

        return new AmbientNameExpression(name, false);
    }
}