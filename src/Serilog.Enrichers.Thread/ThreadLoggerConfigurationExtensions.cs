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
using System.Threading;
using Serilog.Configuration;
using Serilog.Enrichers;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add enrichers for <see cref="System.Thread"/>.
    /// capabilities.
    /// </summary>
    public static class ThreadLoggerConfigurationExtensions
    {
        /// <summary>
        /// Enrich log events with a ThreadId property containing the current <see cref="Thread.ManagedThreadId"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration WithThreadId(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ThreadIdEnricher>();
        }
    }
}