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

using Microsoft.WindowsAzure.Storage.Table;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureTableStorage
{
	/// <summary>
	/// Converts <see cref="LogEventProperty"/> values into simple scalars,
	/// dictionaries and lists so that they can be persisted in Azure Table Storage.
	/// </summary>
	public static class AzurePropertyFormatter
	{
		/// <summary>
		/// Simplify the object so as to make handling the serialized
		/// representation easier.
		/// </summary>
		/// <param name="value">The value to simplify (possibly null).</param>
		/// <param name="format">A format string applied to the value, or null.</param>
		/// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
		/// <returns>An Azure Storage entity EntityProperty</returns>
		public static EntityProperty ToEntityProperty(LogEventPropertyValue value, string format = null, IFormatProvider formatProvider = null)
		{
			var scalar = value as ScalarValue;
			if (scalar != null)
				return SimplifyScalar(scalar.Value);

			var dict = value as DictionaryValue;
			if (dict != null)
			{
				return new EntityProperty(dict.ToString(format, formatProvider));
			}

			var seq = value as SequenceValue;
			if (seq != null)
			{
				return new EntityProperty(seq.ToString(format, formatProvider));
			}
			var str = value as StructureValue;
			if (str != null)
			{
				return new EntityProperty(str.ToString(format, formatProvider));
			}

			return null;
		}

		private static EntityProperty SimplifyScalar(object value)
		{
			if (value == null) return null;

			var valueType = value.GetType();

			if (valueType == typeof(byte[])) return new EntityProperty((byte[])value);
			if (valueType == typeof(bool)) return new EntityProperty((bool)value);
			if (valueType == typeof(DateTimeOffset)) return new EntityProperty((DateTimeOffset)value);
			if (valueType == typeof(DateTime)) return new EntityProperty((DateTime)value);
			if (valueType == typeof(double)) return new EntityProperty((double)value);
			if (valueType == typeof(Guid)) return new EntityProperty((Guid)value);
			if (valueType == typeof(int)) return new EntityProperty((int)value);
			if (valueType == typeof(long)) return new EntityProperty((long)value);
			if (valueType == typeof(string)) return new EntityProperty((string)value);

			return new EntityProperty(value.ToString());
		}

	}
}
