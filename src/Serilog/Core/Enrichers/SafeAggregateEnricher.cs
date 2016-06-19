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
using System.Collections.Generic;
using System.Linq;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Core.Enrichers
{
    class SafeAggregateEnricher : ILogEventEnricher
    {
        readonly ILogEventEnricher[] _enrichers;

        public SafeAggregateEnricher(IEnumerable<ILogEventEnricher> enrichers)
        {
            if (enrichers == null) throw new ArgumentNullException(nameof(enrichers));
            _enrichers = enrichers.ToArray();
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (var enricher in _enrichers)
            {
                try
                {
                    enricher.Enrich(logEvent, propertyFactory);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Exception {0} caught while enriching {1} with {2}.", ex, logEvent, enricher);
                }
            }
        }
    }
}
