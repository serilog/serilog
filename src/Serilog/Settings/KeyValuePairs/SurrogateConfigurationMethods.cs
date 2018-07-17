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
using Serilog.Configuration;
using Serilog.Core;

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



    }
}
