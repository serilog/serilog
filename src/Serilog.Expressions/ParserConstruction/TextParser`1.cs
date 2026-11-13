// Copyright 2016 Datalust, Superpower Contributors, Sprache Contributors
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

using Serilog.ParserConstruction.Model;

namespace Serilog.ParserConstruction;

/// <summary>
/// A parser that consumes text from a string span.
/// </summary>
/// <typeparam name="T">The type of values produced by the parser.</typeparam>
/// <param name="input">The span of text to parse.</param>
/// <returns>A result with a parsed value, or an empty result indicating error.</returns>
delegate Result<T> TextParser<T>(TextSpan input);