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
using System.Threading;
using System.Threading.Tasks;
using Serilog.Events;

#pragma warning disable Serilog004 // Constant MessageTemplate verifier


namespace Serilog.Core
{
    /// <summary>
    /// An add-on for <see cref="Logger"/>, allowing for logging level setting
    /// be adjusted per <see cref="Task"/> scope.
    /// If logging level limits are also provided in the base logger, they
    /// are also taken into account.
    /// </summary>
    internal sealed class ScopedLogger: IScopedLogger, IDisposable
    {
        #region Fields

        private static readonly AsyncLocal<LogEventLevel?> mMinimumLevel = new AsyncLocal<LogEventLevel?>();

        private readonly ILogger mBaseLogger;

        private readonly ILoggingLevelOverrider mOverrider;

        #endregion


        #region Init and clean-up

        /// <summary>
        /// Create an instance of this class based on another logger.
        /// </summary>
        /// <param name="baseLogger">The logger up the food chain</param>
        internal ScopedLogger(ILogger baseLogger)
        {
            mBaseLogger = baseLogger;
            mOverrider = new LoggingLevelOverrider(this);
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            (mBaseLogger as IDisposable)?.Dispose();
        }

        #endregion


        #region ILogger API

        /// <inheritdoc/>
        bool ILogger.IsEnabled(LogEventLevel level)
        {
            var minLevel = mMinimumLevel.Value;
            if (!minLevel.HasValue) return true;

            return level >= minLevel.Value;
        }


        /// <inheritdoc/>
        void ILogger.Write(LogEvent logEvent)
        {
            mBaseLogger.Write(logEvent);
        }

        #endregion


        #region IScopedLogger API

        /// <inheritdoc/>
        IScopedLogger IScopedLogger.SetMinimumLevelOverrider(out ILoggingLevelOverrider overrider)
        {
            overrider = mOverrider;
            return this;
        }


        /// <inheritdoc/>
        public void OverrideMinimumLevel(LogEventLevel minimumLevel)
        {
            mMinimumLevel.Value = minimumLevel;
        }


        /// <inheritdoc/>
        public void OverrideMinimumLevel(string source, LogEventLevel minimumLevel)
        {
        }

        #endregion
    }
}
