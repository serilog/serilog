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



// ReSharper disable UnusedAutoPropertyAccessor.Global, ClassNeverInstantiated.Global

namespace Serilog.ParserConstruction.Display;

/// <summary>
/// Applied to enum members representing tokens to control how they are rendered.
/// </summary>
[AttributeUsage(AttributeTargets.Field|AttributeTargets.Class)]
class TokenAttribute : Attribute
{
    /// <summary>
    /// The category of the token, e.g. "keyword" or "identifier".
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// For tokens that correspond to exact text, e.g. punctuation, the canonical
    /// example of how the token looks.
    /// </summary>
    public string? Example { get; set; }

    /// <summary>
    /// A description of the token, for example "regular expression".
    /// </summary>
    public string? Description { get; set; }
}