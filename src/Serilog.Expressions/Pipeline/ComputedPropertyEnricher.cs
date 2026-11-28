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

using Serilog.Core;
using Serilog.Events;
using Serilog.Expressions;

namespace Serilog.Pipeline;

class ComputedPropertyEnricher : ILogEventEnricher
{
    readonly string _propertyName;
    readonly CompiledExpression _computeValue;

    public ComputedPropertyEnricher(string propertyName, CompiledExpression computeValue)
    {
        _propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        _computeValue = computeValue ?? throw new ArgumentNullException(nameof(computeValue));
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var value = _computeValue(logEvent);
        if (value != null)
            logEvent.AddOrUpdateProperty(new(_propertyName, value));
    }
}