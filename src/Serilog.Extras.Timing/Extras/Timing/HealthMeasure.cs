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
using Serilog.Events;

namespace Serilog.Extras.Timing
{

	sealed class HealthMeasure: IHealthMeasure
	{

		readonly ILogger _logger;
		readonly string _name;
		readonly Func<HealthCheckResult> _operation;
		readonly LogEventLevel _levelHealthy;
		readonly LogEventLevel _levelUnhealthy;
		readonly string _template;

		public HealthMeasure (ILogger logger, string name, Func<HealthCheckResult> healthFunction, LogEventLevel healthyLevel, LogEventLevel unHealthyLevel, string template)
		{
			if (string.IsNullOrWhiteSpace (name))
				throw new ArgumentNullException ("name");

			if (healthFunction == null)
				throw new ArgumentNullException ("healthFunction");

			_logger = logger;
			_name = name;
			_operation = healthFunction;
			_levelHealthy = healthyLevel;
			_levelUnhealthy = unHealthyLevel;
			_template = template;
		}

		#region IMeasure implementation

		public void Write ()
		{
			HealthCheckResult result;
			LogEventLevel eventLevel;

			try {
				result = _operation ();
				eventLevel = result.IsHealthty ?_levelHealthy: _levelUnhealthy;
			} catch (Exception ex) {
				result = new HealthCheckResult (string.Format ("Unable to execute the health check named '{0}'. See inner exception for more details.", _name), ex);
				eventLevel = LogEventLevel.Error;
			}

			_logger.Write (eventLevel, result.Exception, _template, _name, result.Message, result.IsHealthty);
		}

		#endregion

	}
	
}