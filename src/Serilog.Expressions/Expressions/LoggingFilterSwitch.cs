// Copyright Serilog Contributors
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

using Serilog.Core;
using Serilog.Events;
using Serilog.Expressions.Runtime;

namespace Serilog.Expressions;

/// <summary>
/// A log event filter that can be modified at runtime.
/// </summary>
public class LoggingFilterSwitch : ILogEventFilter
{
    // Reference assignments are atomic. While this class makes
    // no attempt to synchronize Expression, ToString(), and IsIncluded(),
    // for any observer, this at least ensures they won't be permanently out-of-sync for
    // all observers.
    volatile Tuple<string, CompiledExpression>? _filter;

    /// <summary>
    /// Construct a <see cref="LoggingFilterSwitch"/>, optionally initialized
    /// with the <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">A filter expression against which log events will be tested.
    /// Only expressions that evaluate to <c>true</c> are included
    /// by the filter. A <c>null</c> expression will accept all
    /// events.</param>
    public LoggingFilterSwitch(string? expression = null)
    {
        Expression = expression;
    }

    /// <summary>
    /// A filter expression against which log events will be tested.
    /// Only expressions that evaluate to <c>true</c> are included
    /// by the filter. A <c>null</c> expression will accept all
    /// events.
    /// </summary>
    public string? Expression
    {
        // ReSharper disable once UnusedMember.Global
        get
        {
            var filter = _filter;
            return filter?.Item1;
        }
        set
        {
            if (value == null)
            {
                _filter = null;
            }
            else
            {
                _filter = new(
                    value,
                    SerilogExpression.Compile(value));
            }
        }
    }

    /// <inheritdoc/>
    public bool IsEnabled(LogEvent logEvent)
    {
        if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

        var filter = _filter;

        if (filter == null)
            return true;

        return Coerce.IsTrue(filter.Item2(logEvent));
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var filter = _filter;
        return filter?.Item1 ?? "";
    }
}