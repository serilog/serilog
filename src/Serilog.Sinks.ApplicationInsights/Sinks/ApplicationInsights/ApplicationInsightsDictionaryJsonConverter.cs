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
using Microsoft.ApplicationInsights.Tracing.Serialization;
using Newtonsoft.Json;

namespace Serilog.Sinks.ApplicationInsights
{
    /// <summary>
    /// Pipes JsonConversion through the Microsoft <see cref="DictionaryConverter"/>.
    /// </summary>
    public class ApplicationInsightsDictionaryJsonConverter : JsonConverter
    {
        /// <summary>
        /// Gets the singleton instance. Not pretty, but simply adhering to the MS implementation style (as this is forwarding to it).
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static ApplicationInsightsDictionaryJsonConverter Instance
        {
            get { return instance.Value; }
        }

        private static readonly Lazy<ApplicationInsightsDictionaryJsonConverter> instance = new Lazy<ApplicationInsightsDictionaryJsonConverter>(() => new ApplicationInsightsDictionaryJsonConverter());

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationInsightsDictionaryJsonConverter"/> class from being created.
        /// </summary>
        private ApplicationInsightsDictionaryJsonConverter() { }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return DictionaryConverter.Instance.CanConvert(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Dictionary de-serialization is not implemented.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Dictionary de-serialization is not implemented.");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="System.ArgumentNullException">serializer</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");

            var flag = serializer.Converters.Remove(this);

            try
            {
                ObjectConverter.Instance.WriteJson(writer, value, serializer);
            }
            finally
            {
                if (flag)
                {
                    serializer.Converters.Add(this);
                }
            }
        }
    }
}