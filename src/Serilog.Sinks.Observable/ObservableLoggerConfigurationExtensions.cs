// Copyright 2013-2016 Serilog Contributors
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
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Observable;

namespace Serilog
{
    public static class ObservableLoggerConfigurationExtensions
    {
        /// <summary>
        /// Write events to Rx observers.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="configureObservers">An action that provides an observable
        /// to which observers can subscribe.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Observers(
            this LoggerSinkConfiguration sinkConfiguration,
            Action<IObservable<LogEvent>> configureObservers,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            if (configureObservers == null) throw new ArgumentNullException(nameof(configureObservers));
            var observable = new ObservableSink();
            configureObservers(observable);
            return sinkConfiguration.Sink(observable, restrictedToMinimumLevel, levelSwitch);
        }
    }
}