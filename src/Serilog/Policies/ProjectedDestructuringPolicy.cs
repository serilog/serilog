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

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Policies
{
    class ProjectedDestructuringPolicy : IDestructuringPolicy
    {
        readonly Func<Type, bool> _canApply;
        readonly Func<object, object> _projection;

        public ProjectedDestructuringPolicy(Func<Type, bool> canApply, Func<object, object> projection)
        {
            if (canApply == null) throw new ArgumentNullException(nameof(canApply));
            if (projection == null) throw new ArgumentNullException(nameof(projection));
            _canApply = canApply;
            _projection = projection;
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!_canApply(value.GetType()))
            {
                result = null;
                return false;
            }

            var projected = _projection(value);
            result = propertyValueFactory.CreatePropertyValue(projected, true);
            return true;
        }
    }
}
