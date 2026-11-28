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

namespace Serilog.Expressions.Ast;

/// <summary>
/// An indexer wildcard is a placeholder in a property path expression. For example,
/// in <code>Headers[?] = 'test'</code>, the question-mark token is a wildcard that means "any property of
/// the <code>Headers</code> object". The other wildcard indexer is the asterisk, meaning "all".
/// </summary>
class IndexerWildcardExpression : Expression
{
    public IndexerWildcardExpression(IndexerWildcard wildcard)
    {
        Wildcard = wildcard;
    }

    public IndexerWildcard Wildcard { get; }

    public override string ToString()
    {
        switch (Wildcard)
        {
            case IndexerWildcard.Any:
                return "?";
            case IndexerWildcard.All:
                return "*";
            default:
                throw new NotSupportedException("Unrecognized wildcard " + Wildcard);
        }
    }
}