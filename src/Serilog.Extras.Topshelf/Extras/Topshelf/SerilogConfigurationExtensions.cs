// Copyright 2007-2012 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace Serilog.Extras.Topshelf
{
    /// <summary>
    ///  Extensions for configuring Logging for Serilog
    /// </summary>
    public static class SerilogConfigurationExtensions
    {
        /// <summary>
        ///   Specify that you want to use the Serilog logging engine.
        /// </summary>
        /// <param name="configurator"></param>
        public static void UseSerilog(this HostConfigurator configurator)
        {
            HostLogger.UseLogger(new SerilogHostLoggerConfigurator());
        }

        /// <summary>
        ///   Specify that you want to use the Serilog logging engine.
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="logger">Serilog logger to use.</param>
        public static void UseSerilog(this HostConfigurator configurator, ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            HostLogger.UseLogger(new SerilogHostLoggerConfigurator(logger));
        }
    }
}
