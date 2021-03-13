// Copyright 2013-2021 Serilog Contributors
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
using System.Threading;
using System.Threading.Tasks;
using Serilog.Events;


namespace Serilog.Core
{
    /// <summary>
    /// An add-on for <see cref="Logger"/>, allowing for logging level setting
    /// be adjusted more openly due to <see cref="ILoggingLevelOverrider"/> interface.
    /// If logging level limits are also provided in the base logger, they
    /// are also taken into account.
    /// </summary>
    internal sealed class OpenSwitchLogger: IOpenSwitchLogger, IDisposable
    {
        #region Fields

        private readonly ILogger mBaseLogger;

        private ILoggingLevelOverrider mDefaultOverrider;

        private LevelOverrideMap<ILoggingLevelOverrider> mOverrideMap;

        #endregion


        #region Init and clean-up

        /// <summary>
        /// Create an instance of this class based on another logger.
        /// </summary>
        /// <param name="baseLogger">The logger up the food chain</param>
        internal OpenSwitchLogger(ILogger baseLogger)
        {
            mBaseLogger = baseLogger;
        }


        /// <summary>
        /// Create an instance of this class enriched with something.
        /// </summary>
        /// <param name="baseLogger">The logger up the food chain</param>
        /// <param name="defaultOverrider">The default level switch if there is no match in the map</param>
        /// <param name="overrideMap">A collection of namespace - level switch</param>
        private OpenSwitchLogger(
            ILogger baseLogger, ILoggingLevelOverrider defaultOverrider,
            LevelOverrideMap<ILoggingLevelOverrider> overrideMap)
        {
            mBaseLogger = baseLogger;
            mDefaultOverrider = defaultOverrider;
            mOverrideMap = overrideMap;
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            (mBaseLogger as IDisposable)?.Dispose();
        }

        #endregion


        #region ILogger API

        /// <inheritdoc/>
        ILogger ILogger.ForContext(string propertyName, object value, bool destructureObjects)
        {
            // Take all level overrides in the base logger
            var baseLogger = mBaseLogger.ForContext(propertyName, value, destructureObjects);
            var levelSwitch = mDefaultOverrider;

            if (propertyName == Constants.SourceContextPropertyName && mOverrideMap != null && value is string context)
            {
                mOverrideMap.GetEffectiveLevel(context, out var _, out levelSwitch);
            }

            return new OpenSwitchLogger(baseLogger, levelSwitch, mOverrideMap);
        }


        /// <inheritdoc/>
        bool ILogger.IsEnabled(LogEventLevel level)
        {
            if (mDefaultOverrider is null) return true;

            return level >= mDefaultOverrider.MinimumLevel;
        }


        /// <inheritdoc/>
        void ILogger.Write(LogEvent logEvent)
        {
            // This will call IsEnabled()
            mBaseLogger.Write(logEvent);
        }

        #endregion


        #region IScopedLogger API

        /// <inheritdoc/>
        IOpenSwitchLogger IOpenSwitchLogger.MinimumLevelOverride(ILoggingLevelOverrider overrider)
        {
            mDefaultOverrider = overrider;
            return this;
        }


        /// <inheritdoc/>
        IOpenSwitchLogger IOpenSwitchLogger.MinimumLevelOverride(string source, ILoggingLevelOverrider overrider)
        {
            if (mOverrideMap is null)
            {
                mOverrideMap = new LevelOverrideMap<ILoggingLevelOverrider>(
                    new Dictionary<string, ILoggingLevelOverrider> { {source, overrider } },
                    LevelAlias.Minimum,
                    mDefaultOverrider);
            }
            else
            {
                mOverrideMap.Add(source, overrider);
            }

            return this;
        }

        #endregion
    }
}
