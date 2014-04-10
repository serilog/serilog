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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serilog.Events;

namespace Serilog.Sinks.MSSQL
{
    /// <summary>
    ///     Converts <see cref="LogEventProperty" /> values into simple scalars,
    ///     dictionaries and lists so that they can be persisted in MSSQL.
    /// </summary>
    public static class XmlPropertyFormatter
    {
        /// <summary>
        ///     Simplify the object so as to make handling the serialized
        ///     representation easier.
        /// </summary>
        /// <param name="value">The value to simplify (possibly null).</param>
        /// <returns>A simplified representation.</returns>
        public static string Simplify(LogEventPropertyValue value)
        {
            var scalar = value as ScalarValue;
            if (scalar != null)
                return SimplifyScalar(scalar.Value);

            var dict = value as DictionaryValue;
            if (dict != null)
            {
                var sb = new StringBuilder();

                sb.Append("<dictionary>");

                foreach (var element in dict.Elements)
                {
                    var key = SimplifyScalar(element.Key);
                    sb.AppendFormat("<item key='{0}'>{1}</item>", key, Simplify(element.Value));
                }

                sb.Append("</dictionary>");

                return sb.ToString();
            }

            var seq = value as SequenceValue;
            if (seq != null)
            {
                var sb = new StringBuilder();

                sb.Append("<sequence>");

                foreach (LogEventPropertyValue element in seq.Elements)
                {
                    sb.AppendFormat("<item>{0}</item>", Simplify(element));
                }

                sb.Append("</sequence>");

                return sb.ToString();
            }

            var str = value as StructureValue;
            if (str != null)
            {
                Dictionary<string, string> props = str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));

                var sb = new StringBuilder();

                sb.AppendFormat("<structure type='{0}'>", str.TypeTag);

                foreach (var element in props)
                {
                    sb.AppendFormat("<property key='{0}'>{1}</property>", element.Key, element.Value);
                }

                sb.Append("</structure>");

                return sb.ToString();
            }

            return null;
        }

        private static string SimplifyScalar(object value)
        {
            if (value == null) return null;

            return value.ToString();
        }
    }
}