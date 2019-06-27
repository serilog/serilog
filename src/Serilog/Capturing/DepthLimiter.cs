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
        interface IDepthLimiter : ILogEventPropertyValueFactory
        {
            LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring);
            IDepthLimiter NextLevel { get; }
        }

        class DepthLimiter : IDepthLimiter
        {
            class NullLimiter : IDepthLimiter
            {
                public static NullLimiter Instance = new NullLimiter();

                NullLimiter() { }

                public LogEventPropertyValue CreatePropertyValue(object value, bool destructureObjects = false)
                {
                    SelfLog.WriteLine("Maximum destructuring depth reached.");
                    return new ScalarValue(null);
                }

                public LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring)
                {
                    SelfLog.WriteLine("Maximum destructuring depth reached.");
                    return new ScalarValue(null);
                }

                public IDepthLimiter NextLevel => null;
            }

            readonly PropertyValueConverter _propertyValueConverter;

            public DepthLimiter(int maximumDepth, PropertyValueConverter propertyValueConverter)
            {
                _propertyValueConverter = propertyValueConverter;

                NextLevel = maximumDepth > 1 ? (IDepthLimiter)new DepthLimiter(maximumDepth - 1, propertyValueConverter) : NullLimiter.Instance;
            }

            public LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring)
            {
                return _propertyValueConverter.CreatePropertyValue(value, destructuring, NextLevel);
            }

            public IDepthLimiter NextLevel { get; }

            LogEventPropertyValue ILogEventPropertyValueFactory.CreatePropertyValue(object value, bool destructureObjects)
            {
                return _propertyValueConverter.CreatePropertyValue(value, destructureObjects, NextLevel);
            }
        }
    }
}
