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
/// An AST node.
/// </summary>
abstract class Expression
{
    /// <summary>
    /// The <see cref="ToString"/> representation of an <see cref="Expression"/> is <strong>not</strong>
    /// guaranteed to be syntactically valid: this is provided for debugging purposes only.
    /// </summary>
    /// <returns>A textual representation of the expression.</returns>
    public abstract override string ToString();
}