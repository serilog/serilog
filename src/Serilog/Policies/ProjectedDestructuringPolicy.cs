// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Policies;

class ProjectedDestructuringPolicy<T> : IDestructuringPolicy
{
    readonly Func<Type, bool>? _canApply;
    readonly Func<T, object> _projection;

    public ProjectedDestructuringPolicy(Func<Type, bool>? canApply, Func<T, object> projection)
    {
        _canApply = canApply;
        _projection = Guard.AgainstNull(projection);
    }

    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        Guard.AgainstNull(value);

        if (value is T typed)
        {
            if (_canApply == null)
            {
                Convert(propertyValueFactory, out result, typed);
                return true;
            }

            if (_canApply(typed.GetType()))
            {
                Convert(propertyValueFactory, out result, typed);
                return true;
            }
        }

        result = null;
        return false;
    }

    void Convert(ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result, T typed)
    {
        var projected = _projection(typed);
        result = propertyValueFactory.CreatePropertyValue(projected, destructureObjects: true);
    }
}
