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

using System;

using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Capturing
{
    partial class PropertyValueConverter
    {
        class DepthLimiter : ILogEventPropertyValueFactory
        {
            [ThreadStatic]
            static int _currentDepth;
            
            readonly int _maximumDestructuringDepth;
            readonly PropertyValueConverter _propertyValueConverter;

            public DepthLimiter(int maximumDepth, PropertyValueConverter propertyValueConverter)
            {
                _maximumDestructuringDepth = maximumDepth;
                _propertyValueConverter = propertyValueConverter;
            }

            public void SetCurrentDepth(int depth)
            {
                _currentDepth = depth;
            }

            public LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring)
            {
                var storedDepth = _currentDepth;

                var result = DefaultIfMaximumDepth(storedDepth) ??
                    _propertyValueConverter.CreatePropertyValue(value, destructuring, storedDepth + 1);

                _currentDepth = storedDepth;

                return result;
            }

            LogEventPropertyValue ILogEventPropertyValueFactory.CreatePropertyValue(object value, bool destructureObjects)
            {
                var storedDepth = _currentDepth;

                var result = DefaultIfMaximumDepth(storedDepth) ??
                    _propertyValueConverter.CreatePropertyValue(value, destructureObjects, storedDepth + 1);

                _currentDepth = storedDepth;

                return result;
            }

            LogEventPropertyValue DefaultIfMaximumDepth(int depth)
            {
                if (depth == _maximumDestructuringDepth)
                {
                    SelfLog.WriteLine("Maximum destructuring depth reached.");
                    return new ScalarValue(null);
                }

                return null;
            }
        }
    }
}
