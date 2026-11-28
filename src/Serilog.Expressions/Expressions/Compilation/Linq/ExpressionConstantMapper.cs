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

using System.Linq.Expressions;
using Serilog.Events;

namespace Serilog.Expressions.Compilation.Linq;

class ExpressionConstantMapper : ExpressionVisitor
{
    readonly IDictionary<object, Expression> _mapping;

    public ExpressionConstantMapper(IDictionary<object, Expression> mapping)
    {
        _mapping = mapping;
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
        if (node.Value is ScalarValue { Value: {} sv } &&
            _mapping.TryGetValue(sv, out var substitute))
            return substitute;

        return base.VisitConstant(node);
    }
}
