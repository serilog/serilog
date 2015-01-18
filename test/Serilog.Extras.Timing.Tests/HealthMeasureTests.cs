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

using NUnit.Framework;
using System;
using Serilog.Events;
using System.Reactive.Linq;

namespace Serilog.Extras.Timing.Tests
{


	[TestFixture ()]
	public class HealthMeasureTests
	{
		LogEvent _eventSeen;

		public HealthMeasureTests ()
		{
			var configuration = new LoggerConfiguration();
			var logger = configuration
				.MinimumLevel.Verbose()               // Make sure we see also the lowest level
				.WriteTo.Observers(events => events   // So we can check the result
					.Do(evt => { _eventSeen = evt; })
					.Subscribe())
				.WriteTo.Console()                    // Still visible in the unit test console
				.CreateLogger();

			Log.Logger = logger;
		}

		[Test ()]
		public void HealthyCheckResultShouldReportInformation ()
		{
			var check = Log.Logger.HealthCheck ("test-healthy", () => new HealthCheckResult ());

			check.Write ();

			Assert.AreEqual (_eventSeen.RenderMessage () , "Health check \"test-healthy\" result is \"successful\".");
			Assert.IsTrue (_eventSeen.Level == LogEventLevel.Information);
		}

		[Test ()]
		public void HealthyCheckWithCustomLevelResultShouldReportCustomLevel()
		{
			var check = Log.Logger.HealthCheck ("test-healthy", () => new HealthCheckResult (), LogEventLevel.Verbose);

			check.Write ();

			Assert.AreEqual (_eventSeen.RenderMessage () , "Health check \"test-healthy\" result is \"successful\".");
			Assert.IsTrue (_eventSeen.Level == LogEventLevel.Verbose);
		}

		[Test ()]
		public void UnHealthyCheckResultShouldReportWarning ()
		{
		
			var check = Log.Logger.HealthCheck ("test-unhealthy", () => new HealthCheckResult ("something was wrong", new ArgumentException()));

			check.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage () == "Health check \"test-unhealthy\" result is \"something was wrong\".");
			Assert.IsTrue (_eventSeen.Level == LogEventLevel.Warning);
		}

		[Test ()]
		public void UnHealthyCheckWithCustomLevelResultShouldReportCustomLevel()
		{

			var check = Log.Logger.HealthCheck ("test-unhealthy", () => new HealthCheckResult ("something was wrong", new ArgumentException()), LogEventLevel.Information, LogEventLevel.Fatal);

			check.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage () == "Health check \"test-unhealthy\" result is \"something was wrong\".");
			Assert.IsTrue (_eventSeen.Level == LogEventLevel.Fatal);
		}

		[Test ()]
		public void HealthyCheckWithExceptionMustBeCaptured ()
		{
		
			var check = Log.Logger.HealthCheck ("test-exception", () => {
				// Something goes wrong here
				throw new ArgumentException();
			});

			check.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage () == "Health check \"test-exception\" result is \"Unable to execute the health check named 'test-exception'. See inner exception for more details.\".");
			Assert.NotNull (_eventSeen.Exception);
			Assert.IsInstanceOf (typeof(ArgumentException), _eventSeen.Exception);
			Assert.IsTrue (_eventSeen.Level == LogEventLevel.Error);
		}

	}
}

