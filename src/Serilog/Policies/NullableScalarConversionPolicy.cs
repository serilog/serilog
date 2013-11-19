// Copyright 2013 Serilog Contributors
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

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Policies
{
    class NullableScalarConversionPolicy : IScalarConversionPolicy
    {
        public bool TryConvertToScalar(object value, ILogEventPropertyValueFactory propertyValueFactory, out ScalarValue result)
        {
            var type = value.GetType();
            if (!type.IsConstructedGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                result = null;
                return false;
            }

            var dynamicValue = (dynamic)value;
            var innerValue = dynamicValue.HasValue ? (object)dynamicValue.Value : null;
            result = propertyValueFactory.CreatePropertyValue(innerValue) as ScalarValue;

            return result != null;
        }
    }
}
