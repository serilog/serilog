// Copyright 2013-2017 Serilog Contributors
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

namespace Serilog.Formatting.Display;

/// <summary>
/// Describes the properties available in standard message template-based
/// output format strings.
/// </summary>
public static class OutputProperties
{
    /// <summary>
    /// The message rendered from the log event.
    /// </summary>
    public const string MessagePropertyName = "Message";

    /// <summary>
    /// The timestamp of the log event.
    /// </summary>
    public const string TimestampPropertyName = "Timestamp";

    /// <summary>
    /// The level of the log event.
    /// </summary>
    public const string LevelPropertyName = "Level";

    /// <summary>
    /// The id of the trace that was active at the log event's time of creation, if any.
    /// </summary>
    public const string TraceIdPropertyName = "TraceId";

    /// <summary>
    /// The id of the span that was active at the log event's time of creation, if any.
    /// </summary>
    public const string SpanIdPropertyName = "SpanId";

    /// <summary>
    /// A new line.
    /// </summary>
    public const string NewLinePropertyName = "NewLine";

    /// <summary>
    /// The exception associated with the log event.
    /// </summary>
    public const string ExceptionPropertyName = "Exception";

    /// <summary>
    /// The properties of the log event.
    /// </summary>
    public const string PropertiesPropertyName = "Properties";
}
