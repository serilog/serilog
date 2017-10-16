// Copyright 2013-2017 Serilog Contributors
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
using System.Linq.Expressions;
using Serilog.Settings.KeyValuePairs;

// must be in namespace Serilog so it shows up when using LoggerConfiguration
namespace Serilog
{
    /// <summary>
    /// Extensions to add a source of settings based on an expression-tree.
    /// </summary>
    public static class ExpressionSettingsSourceExtensions
    {
        /// <summary>
        /// Adds the key-value settings equivalent to the configuration expression
        /// </summary>
        /// <param name="builder">the builder</param>
        /// <param name="expression">an expression that represents the applied configuration</param>
        /// <returns>an builder to allow chaining</returns>
        public static ISettingsSourceBuilder AddExpression(this ISettingsSourceBuilder builder, Expression<Func<LoggerConfiguration, LoggerConfiguration>> expression)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return builder.AddSource(new ExpressionSettingsSource(expression));
        }
    }
}
