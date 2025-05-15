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

class TypeDestructuringPolicy<T> : IDestructuringPolicy
{
    readonly bool _allowInherited;
    readonly Func<T, object> _projection;
    readonly Type _type;

    public TypeDestructuringPolicy(Func<T, object> projection, bool allowInherited)
    {
        _allowInherited = allowInherited;
        _projection = Guard.AgainstNull(projection);
        _type = typeof(T);
    }

    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        Guard.AgainstNull(value);

        if (value is not T typedValue)
        {
            result = null;
            return false;
        }

        if (!_allowInherited)
        {
            if(value.GetType() != _type)
            {
                result = null;
                return false;
            }
        }

        var projected = _projection(typedValue);
        result = propertyValueFactory.CreatePropertyValue(projected, destructureObjects: true);
        return true;
    }
}
