// Copyright 2013 Serilog Contributors
// Based on Topshelf.Log4Net, copyright 2007-2012 Chris Patterson,
// Dru Sellers, Travis Smith, et. al.
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
using Topshelf.Logging;

namespace Serilog.Extras.Topshelf
{
    class SerilogWriterFactory : LogWriterFactory
    {
        readonly ILogger _logger;

        public SerilogWriterFactory(ILogger logger = null)
        {
            _logger = logger;
        }

        public LogWriter Get(string name)
        {
            var contextual = _logger == null ?
                Log.ForContext(Constants.SourceContextPropertyName, name) :
                _logger.ForContext(Constants.SourceContextPropertyName, name);

            return new SerilogWriter(contextual);
        }

        public void Shutdown()
        {
            var toShutDown = (_logger ?? Log.Logger) as IDisposable;
            if (toShutDown != null)
                toShutDown.Dispose();
        }
    }
}

