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
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;

namespace Serilog.Formatting.Display
{
    static class OutputProperties
    {
        public const string MessagePropertyName = "Message";
        public const string TimeStampPropertyName = "TimeStamp";
        public const string LevelPropertyName = "Level";
        public const string NewLinePropertyName = "NewLine";
        public const string ExceptionPropertyName = "Exception";

        public static IReadOnlyDictionary<string, LogEventProperty> GetOutputProperties(LogEvent logEvent)
        {
            var result = logEvent.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            result.Add(MessagePropertyName, new LogEventProperty(MessagePropertyName, new LogEventPropertyMessageValue(logEvent.MessageTemplate, logEvent.Properties)));
            result.Add(TimeStampPropertyName, LogEventProperty.For(TimeStampPropertyName, logEvent.TimeStamp));
            result.Add(LevelPropertyName, LogEventProperty.For(LevelPropertyName, logEvent.Level));
            result.Add(NewLinePropertyName, new LogEventProperty(NewLinePropertyName, new ScalarValue(Environment.NewLine)));

            var exception = logEvent.Exception == null ? "" : (logEvent.Exception + Environment.NewLine);
            result.Add(ExceptionPropertyName, LogEventProperty.For(ExceptionPropertyName, exception));

            return result;
        }
    }
}
