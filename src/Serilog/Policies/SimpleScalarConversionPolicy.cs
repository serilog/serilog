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

namespace Serilog.Policies;

class SimpleScalarConversionPolicy : IScalarConversionPolicy
{
    readonly HashSet<Type> _scalarTypes;

    public SimpleScalarConversionPolicy(IEnumerable<Type> scalarTypes)
    {
        _scalarTypes = [..scalarTypes];
    }

    public bool TryConvertToScalar(object value, [NotNullWhen(true)] out ScalarValue? result)
    {
        if (_scalarTypes.Contains(value.GetType()))
        {
            result = new(value);
            return true;
        }

        result = null;
        return false;
    }
}
