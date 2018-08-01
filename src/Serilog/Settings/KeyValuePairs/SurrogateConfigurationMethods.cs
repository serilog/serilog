// Copyright 2013-2018 Serilog Contributors
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
using System.Reflection;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Settings.KeyValuePairs
{
    /// <summary>
    /// Contains "fake extension" methods for the Serilog configuration API.
    /// By default the settings knows how to find extension methods, but some configuration
    /// are actually "regular" method calls and would not be found otherwise.
    ///
    /// This static class contains internal methods that can be used instead.
    ///
    /// See also <seealso cref="CallableConfigurationMethodFinder"/>
    /// </summary>
    static class SurrogateConfigurationMethods
    {
        static readonly Dictionary<Type, MethodInfo[]> SurrogateMethodCandidates = typeof(SurrogateConfigurationMethods)
            .GetTypeInfo().DeclaredMethods
            .GroupBy(m => m.GetParameters().First().ParameterType)
            .ToDictionary(g => g.Key, g => g.ToArray());

        internal static readonly MethodInfo[] WriteTo = SurrogateMethodCandidates[typeof(LoggerSinkConfiguration)];
        internal static readonly MethodInfo[] AuditTo = SurrogateMethodCandidates[typeof(LoggerAuditSinkConfiguration)];
        internal static readonly MethodInfo[] Enrich = SurrogateMethodCandidates[typeof(LoggerEnrichmentConfiguration)];
        internal static readonly MethodInfo[] Destructure = SurrogateMethodCandidates[typeof(LoggerDestructuringConfiguration)];
        internal static readonly MethodInfo[] Filter = SurrogateMethodCandidates[typeof(LoggerFilterConfiguration)];

        internal static LoggerConfiguration Sink(
            LoggerSinkConfiguration loggerSinkConfiguration,
            ILogEventSink sink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            return loggerSinkConfiguration.Sink(sink, restrictedToMinimumLevel, levelSwitch);
        }

        internal static LoggerConfiguration Sink(
            LoggerAuditSinkConfiguration auditSinkConfiguration,
            ILogEventSink sink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            return auditSinkConfiguration.Sink(sink, restrictedToMinimumLevel, levelSwitch);
        }

        internal static LoggerConfiguration With(LoggerEnrichmentConfiguration loggerEnrichmentConfiguration, ILogEventEnricher enricher)
        {
            return loggerEnrichmentConfiguration.With(enricher);
        }

        internal static LoggerConfiguration FromLogContext(LoggerEnrichmentConfiguration loggerEnrichmentConfiguration)
        {
            return loggerEnrichmentConfiguration.FromLogContext();
        }

        internal static LoggerConfiguration With(LoggerDestructuringConfiguration loggerDestructuringConfiguration,
            IDestructuringPolicy policy)
        {
            return loggerDestructuringConfiguration.With(policy);
        }

        internal static LoggerConfiguration AsScalar(LoggerDestructuringConfiguration loggerDestructuringConfiguration,
            Type scalarType)
        {
            return loggerDestructuringConfiguration.AsScalar(scalarType);
        }

        internal static LoggerConfiguration ToMaximumCollectionCount(LoggerDestructuringConfiguration loggerDestructuringConfiguration,
            int maximumCollectionCount)
        {
            return loggerDestructuringConfiguration.ToMaximumCollectionCount(maximumCollectionCount);
        }

        internal static LoggerConfiguration ToMaximumDepth(LoggerDestructuringConfiguration loggerDestructuringConfiguration,
            int maximumDestructuringDepth)
        {
            return loggerDestructuringConfiguration.ToMaximumDepth(maximumDestructuringDepth);
        }

        internal static LoggerConfiguration ToMaximumStringLength(LoggerDestructuringConfiguration loggerDestructuringConfiguration,
            int maximumStringLength)
        {
            return loggerDestructuringConfiguration.ToMaximumStringLength(maximumStringLength);
        }

        internal static LoggerConfiguration With(LoggerFilterConfiguration loggerFilterConfiguration,
            ILogEventFilter filter)
        {
            return loggerFilterConfiguration.With(filter);
        }
    }
}
