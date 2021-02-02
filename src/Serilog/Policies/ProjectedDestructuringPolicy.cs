
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
    /// <summary>
    /// When destructuring objects, projects instances of the specified type with
    /// the provided function.
    /// </summary>
    public class ProjectedDestructuringPolicy : IDestructuringPolicy
    {
        readonly Func<Type, bool> _canApply;
        readonly Func<object, object> _projection;

        /// <summary>
        /// Construct policy
        /// </summary>
        /// <param name="canApply">
        /// Predicate for object type to check if policy can be applied.
        /// </param>
        /// <param name="projection">
        /// Projection function transforming incoming type into the projected one.
        /// </param>
        public ProjectedDestructuringPolicy(Func<Type, bool> canApply, Func<object, object> projection)
        {
            _canApply = canApply ?? throw new ArgumentNullException(nameof(canApply));
            _projection = projection ?? throw new ArgumentNullException(nameof(projection));
        }

        /// <summary>
        /// Implements <see cref="IDestructuringPolicy.TryDestructure(object, ILogEventPropertyValueFactory, out LogEventPropertyValue)"/>.
        /// </summary>
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!_canApply(value.GetType()))
            {
                result = null;
                return false;
            }

            var projected = _projection(value);
            result = propertyValueFactory.CreatePropertyValue(projected, destructureObjects: true);
            return true;
        }
    }
}
