// Copyright 2014 Serilog Contributors
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
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;

namespace Serilog.Sinks.XSockets.Sinks.XSockets
{
    public static class XSocketsPropertyFormatter
    {
        static readonly HashSet<Type> XSocketsSpecialScalars = new HashSet<Type>
        {
            typeof(bool),
            typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
            typeof(byte[])
        };

        /// <summary>
        /// Simplify the object so as to make handling the serialized
        /// representation easier.
        /// </summary>
        /// <param name="value">The value to simplify (possibly null).</param>
        /// <returns>A simplified representation.</returns>
        public static object Simplify(LogEventPropertyValue value)
        {
            var scalar = value as ScalarValue;
            if (scalar != null)
                return SimplifyScalar(scalar.Value);

            var dict = value as DictionaryValue;
            if (dict != null)
                return dict
                    .Elements
                    .ToDictionary(kv => SimplifyScalar(kv.Key), kv => Simplify(kv.Value));

            var seq = value as SequenceValue;
            if (seq != null)
                return seq.Elements.Select(Simplify).ToArray();

            var str = value as StructureValue;
            if (str != null)
            {
                var props = str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));
                if (str.TypeTag != null)
                    props["$typeTag"] = str.TypeTag;
                return props;
            }

            return null;
        }

        static object SimplifyScalar(object value)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();
            if (XSocketsSpecialScalars.Contains(valueType))
                return value;

            return value.ToString();
        }
    }
}
