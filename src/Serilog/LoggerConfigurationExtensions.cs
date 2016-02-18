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
using System.ComponentModel;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Formatting.Raw; 

#if PROCESS
using System.Diagnostics;
#endif

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework
    /// capabilities.
    /// </summary>
    public static class LoggerConfigurationExtensions
    {
         
        /// <summary>
        /// Enrich log events with properties from <see cref="Context.LogContext"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration FromLogContext(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<LogContextEnricher>();
        }

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

#if PROCESS
        /// <summary>
        /// Enrich log events with a ProcessId property containing the current <see cref="Process.Id"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithProcessId(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ProcessIdEnricher>();
        }
#endif

#if !DOTNET5_1
        /// <summary>
        /// Enrich log events with a MachineName property containing the current <see cref="Environment.MachineName"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithMachineName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<MachineNameEnricher>();
        }

        /// <summary>
        /// Enriches log events with an EnvironmentUserName property containing [<see cref="Environment.UserDomainName"/>\]<see cref="Environment.UserName"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithEnvironmentUserName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<EnvironmentUserNameEnricher>();
        }
#endif
    }
}
