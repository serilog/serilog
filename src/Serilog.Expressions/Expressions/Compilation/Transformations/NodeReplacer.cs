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

namespace Serilog.Expressions.Compilation.Transformations;

class NodeReplacer : IdentityTransformer
{
    readonly Expression _source;
    readonly Expression _dest;

    public static Expression Replace(Expression expr, Expression source, Expression dest)
    {
        var replacer = new NodeReplacer(source, dest);
        return replacer.Transform(expr);
    }

    NodeReplacer(Expression source, Expression dest)
    {
        _source = source;
        _dest = dest;
    }

    protected override Expression Transform(Expression x)
    {
        if (x == _source)
            return _dest;

        return base.Transform(x);
    }
}