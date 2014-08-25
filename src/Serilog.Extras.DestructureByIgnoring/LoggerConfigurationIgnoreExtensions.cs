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
using System.Linq.Expressions;
using Serilog.Configuration;
using Serilog.Extras.DestructureByIgnoring.Extras.DestructureByIgnoring;

namespace Serilog.Extras.DestructureByIgnoring
{    
    /// <summary>
    /// Adds the Destructure.ByIgnoringProperties() extension to <see cref="LoggerDestructuringConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationIgnoreExtensions
    {
        /// <summary>
        /// Destructure.ByIgnoringProperties takes one or more expressions that access a property, e.g. obj => obj.Property, and uses the property names to determine which
        /// properties are ignored when an object of type TDestruture is destructured by serilog.
        /// </summary>
        /// <param name="configuration">The logger configuration to apply configuration to.</param>
        /// <param name="ignoredProperty">The function expressions that expose the properties to ignore.</param>
        /// <returns>An object allowing configuration to continue.</returns>
        public static LoggerConfiguration ByIgnoringProperties<TDestruture>(this LoggerDestructuringConfiguration configuration, params Expression<Func<TDestruture, object>>[] ignoredProperty)
        {
            return configuration.With(new DestructureByIgnoringPolicy<TDestruture>(ignoredProperty));
        }
    }
}
