// Copyright 2014 Serilog Contributors
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

using System;
using System.Linq.Expressions;

namespace Serilog.Extras.DestructureByIgnoring
{
    static class IgnoredPropertyExpressionExtensions
    {
        private const string expressionNotSupported = "A property name cannot be retrieved from function expression with body of type {0}. " +
                                                      "Only function expressions that access a property are currently supported. e.g. obj => obj.Property";

        public static string GetPropertyNameFromExpression<TDestructureType>(this Expression<Func<TDestructureType, object>> ignoredProperty)
        {
            var expressionBody = ignoredProperty.Body;

            var memberExpression = GetMemberExpression(expressionBody);

            var isNotSimplePropertyAccess = memberExpression == null || GetMemberExpression(memberExpression.Expression) != null;
            if (isNotSimplePropertyAccess)
            {
                throw new ArgumentException(string.Format(expressionNotSupported,
                    expressionBody.GetType().Name), "ignoredProperty");
            }

            return memberExpression.Member.Name;
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            return GetMemberExpressionForValueType(expression) ?? GetMemberExpressionForReferenceType(expression);
        }

        private static MemberExpression GetMemberExpressionForValueType(Expression expression)
        {
            var bodyOfExpression = expression as UnaryExpression;

            return bodyOfExpression != null ? bodyOfExpression.Operand as MemberExpression : null;
        }

        private static MemberExpression GetMemberExpressionForReferenceType(Expression expression)
        {
            return expression as MemberExpression;
        }
    }
}