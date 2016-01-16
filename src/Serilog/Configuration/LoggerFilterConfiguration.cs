// Copyright 2013-2015 Serilog Contributors
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
using Serilog.Core.Filters;
using Serilog.Events;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls filter configuration.
    /// </summary>
    public class LoggerFilterConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogEventFilter> _addFilter;

        internal LoggerFilterConfiguration(
            LoggerConfiguration loggerConfiguration,
            Action<ILogEventFilter> addFilter)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (addFilter == null) throw new ArgumentNullException(nameof(addFilter));
            _loggerConfiguration = loggerConfiguration;
            _addFilter = addFilter;
        }


        /// <summary>
        /// Filter out log events from the stream based on the provided filter.
        /// </summary>
        /// <param name="filters">The filters to apply.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With(params ILogEventFilter[] filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            foreach (var logEventFilter in filters)
            {
                if (logEventFilter == null)
                    throw new ArgumentException("Null filter is not allowed.");
                _addFilter(logEventFilter);
            }
            return _loggerConfiguration;
        }

        /// <summary>
        /// Filter out log events from the stream based on the provided filter.
        /// </summary>
        /// <typeparam name="TFilter">The filters to apply.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With<TFilter>()
            where TFilter : ILogEventFilter, new()
        {
            return With(new TFilter());
        }

        /// <summary>
        /// Filter out log events that match a predicate.
        /// </summary>
        /// <param name="exclusionPredicate">Function that returns true when an event
        /// should be excluded (silenced).</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration ByExcluding(Func<LogEvent, bool> exclusionPredicate)
        {
            return With(new DelegateFilter(logEvent => !exclusionPredicate(logEvent)));
        }

        /// <summary>
        /// Filter log events to include only those that match a predicate.
        /// </summary>
        /// <param name="inclusionPredicate">Function that returns true when an event
        /// should be included (emitted).</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration ByIncludingOnly(Func<LogEvent, bool> inclusionPredicate)
        {
            return With(new DelegateFilter(inclusionPredicate));
        }
    }
}

