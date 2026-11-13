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

// ReSharper disable IntroduceOptionalParameters.Global, MemberCanBePrivate.Global, UnusedAutoPropertyAccessor.Global

namespace Serilog.ParserConstruction;

/// <summary>
/// Represents an error that occurs during parsing.
/// </summary>
class ParseException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorPosition">The position of the error in the input text.</param>
    public ParseException(string message, Position errorPosition) : this(message, errorPosition, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorPosition">The position of the error in the input text.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ParseException(string message, Position errorPosition, Exception? innerException) : base(message, innerException)
    {
        ErrorPosition = errorPosition;
    }

    /// <summary>
    /// The position of the error in the input text, or <see cref="Position.Empty"/> if no position is specified.
    /// </summary>
    public Position ErrorPosition { get; }
}