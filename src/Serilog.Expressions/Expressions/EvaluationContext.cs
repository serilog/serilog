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

using Serilog.Events;
using Serilog.Expressions.Runtime;

namespace Serilog.Expressions;

readonly struct EvaluationContext
{
    public LogEvent LogEvent { get; }
    public Locals? Locals { get; }

    public EvaluationContext(LogEvent logEvent, Locals? locals = null)
    {
        LogEvent = logEvent ?? throw new ArgumentNullException(nameof(logEvent));
        Locals = locals;
    }
}