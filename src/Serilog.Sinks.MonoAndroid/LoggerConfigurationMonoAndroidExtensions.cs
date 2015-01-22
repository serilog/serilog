// Copyright 2015 Serilog Contributors
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
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.MonoAndroid;

namespace Serilog
{
	/// <summary>
	/// Adds WriteTo.AndroidLog() to the logger configuration.
	/// </summary>
	public static class LoggerConfigurationMonoAndroidExtensions
	{
		const string DefaultAndroidLogOutputTemplate = "[{Level}] {Message:l{NewLine:l}{Exception:l}";

	    /// <summary>
	    /// Write to the built-in Android log.
	    /// </summary>
	    /// <param name="sinkConfiguration">The configuration this applies to.</param>
	    /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
	    /// <param name="outputTemplate">Output template providing the format for events</param>
	    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
	    /// <returns>Logger configuration, allowing configuration to continue.</returns>
	    /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
	    public static LoggerConfiguration AndroidLog(this LoggerSinkConfiguration sinkConfiguration,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			string outputTemplate = DefaultAndroidLogOutputTemplate,
			IFormatProvider formatProvider = null)
		{

			if (sinkConfiguration == null)
				throw new ArgumentNullException("sinkConfiguration");

			if (outputTemplate == null)
				throw new ArgumentNullException("outputTemplate");

			var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
			return sinkConfiguration.Sink(new AndroidLogSink(formatter), restrictedToMinimumLevel);
		}
	}
}