// Copyright 2013 Nicholas Blumhardt
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
using Serilog.Parsing;

namespace Serilog.Events
{
    public class LogEventProperty
    {
        private readonly string _name;

        public static LogEventProperty For(string name, object value, bool destructureObjects = false)
        {
            return new LogEventProperty(name, LogEventPropertyValue.For(value,
                    destructureObjects ?
                        Destructuring.Destructure :
                        Destructuring.Default));
        }

        public LogEventProperty(string name, LogEventPropertyValue value)
        {
            if (!IsValidName(name))
                throw new ArgumentException("Property name is not valid.");

            _name = name;
            Value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public LogEventPropertyValue Value { get; set; }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}