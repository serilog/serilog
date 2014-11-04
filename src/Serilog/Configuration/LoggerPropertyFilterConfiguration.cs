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
using Serilog.Core;
using Serilog.Core.Filters;
using Serilog.Events;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls filter configuration.
    /// </summary>
    public class LoggerPropertyFilterConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogPropertyFilter> _addFilter;

        internal LoggerPropertyFilterConfiguration(
            LoggerConfiguration loggerConfiguration,
            Action<ILogPropertyFilter> addFilter)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (addFilter == null) throw new ArgumentNullException("addFilter");
            _loggerConfiguration = loggerConfiguration;
            _addFilter = addFilter;
        }


        /// <summary>
        /// Filter properties based on the provided filters.
        /// </summary>
        /// <param name="filters">The filters to apply.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With(params ILogPropertyFilter[] filters)
        {
            if (filters == null) throw new ArgumentNullException("filters");
            foreach (var logPropertyFilter in filters)
            {
                if (logPropertyFilter == null)
                    throw new ArgumentException("Null filter is not allowed.");
                _addFilter(logPropertyFilter);
            }
            return _loggerConfiguration;
        }

        /// <summary>
        /// Filter properties based on the provided filter.
        /// </summary>
        /// <typeparam name="TFilter">The filters to apply.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With<TFilter>()
            where TFilter : ILogPropertyFilter, new()
        {
            return With(new TFilter());
        }

        /// <summary>
        /// Filters null property values
        /// </summary>
        private class NullPropertySuppressionFilter : ILogPropertyFilter
        {
            public static readonly NullPropertySuppressionFilter instance = new NullPropertySuppressionFilter();
            public bool IsAllowed(string propertyName, object value)
            {
                return null != value;
            }
        }

        /// <summary>
        /// Filter out properties that are null
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration SuppressNulls()
        {
            return With(NullPropertySuppressionFilter.instance);
        }
    }
}

