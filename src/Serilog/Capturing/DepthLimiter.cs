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
            static readonly DepthLimiter NullInstance = new DepthLimiter();

            readonly PropertyValueConverter _propertyValueConverter;

            public readonly DepthLimiter NextLevel;

            DepthLimiter(int maximumDepth, PropertyValueConverter propertyValueConverter)
            {
                _propertyValueConverter = propertyValueConverter;

                NextLevel = Create(maximumDepth, propertyValueConverter);
            }

            /// <summary>
            /// Null object.
            /// </summary>
            DepthLimiter ()
            { }

            public static DepthLimiter Create(int maximumDepth, PropertyValueConverter propertyValueConverter)
            {
                return maximumDepth > 1 ? new DepthLimiter(maximumDepth - 1, propertyValueConverter) : NullInstance;
            }

            public LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring)
            {
                if (_propertyValueConverter == null)
                {
                    SelfLog.WriteLine("Maximum destructuring depth reached.");
                    return new ScalarValue(null);
                }

                return _propertyValueConverter.CreatePropertyValue(value, destructuring, NextLevel);
            }

            LogEventPropertyValue ILogEventPropertyValueFactory.CreatePropertyValue(object value, bool destructureObjects)
            {
                if (_propertyValueConverter == null)
                {
                    SelfLog.WriteLine("Maximum destructuring depth reached.");
                    return new ScalarValue(null);
                }

                return _propertyValueConverter.CreatePropertyValue(value, destructureObjects, NextLevel);
            }
        }
    }
}
